using AutoMapper;
using LibraryManagement.Application.Common.Models;
using LibraryManagement.Application.DTOs.BookDTOs;
using LibraryManagement.Application.DTOs.BorrowingRequestDTOs;
using LibraryManagement.Application.Interfaces.Repositories;
using LibraryManagement.Application.Interfaces.Services;
using LibraryManagement.Domain.Enum;
using LibraryManagement.Domain.Models;

namespace LibraryManagement.Infrastructure.Services;

public class BookBorrowingService : IBookBorrowingRequestService
{
    private readonly IBookBorrowingRequestRepository _requestRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IBookBorrowingRequestDetailsRepository _detailsRepository;
    private readonly IMapper _mapper;

    public BookBorrowingService(IBookBorrowingRequestRepository requestRepository, IBookRepository bookRepository,
        IBookBorrowingRequestDetailsRepository detailsRepository, IMapper mapper)
    {
        _requestRepository = requestRepository;
        _bookRepository = bookRepository;
        _detailsRepository = detailsRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<BorrowingRequestResponse>> GetRequests(int pageNumber, int pageSize,
        string searchTerm = "")
    {
        var requests = await _requestRepository.GetAllWithUserAsync();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            requests = requests.Where(r =>
                r.User.FirstName.Contains(searchTerm) || r.User.LastName.Contains(searchTerm) ||
                r.Status.ToString().Contains(searchTerm));
        }

        var paginatedRequests = PaginatedList<BookBorrowingRequest>.Create(requests, pageNumber, pageSize);

        var mappedRequests = paginatedRequests.Items.Select(r => _mapper.Map<BorrowingRequestResponse>(r)).ToList();

        return new PaginatedList<BorrowingRequestResponse>(mappedRequests, paginatedRequests.TotalCount, pageNumber,
            pageSize);
    }

    public async Task<BorrowingRequestResponse> GetRequestByIdAsync(Guid requestId)
    {
        var request = await _requestRepository.GetByIdAsync(requestId);

        if (request == null)
        {
            return null;
        }

        var response = _mapper.Map<BorrowingRequestResponse>(request);

        response.BookIds = request.BookBorrowingRequestDetails.Select(detail => detail.BookId).ToList();
        return response;
    }

    public async Task<PaginatedList<BookResponse>> GetUserBorrowedBooks(Guid userId, int pageNumber, int pageSize,
        string searchTerm = "")
    {
        var borrowingRequests = await _requestRepository.GetAllRequestsByUserAsync(userId);

        var borrowedBooks = new List<BookResponse>();

        foreach (var request in borrowingRequests)
        {
            foreach (var detail in request.BookBorrowingRequestDetails)
            {
                var book = await _bookRepository.GetByIdAsync(detail.BookId);
                var bookResponse = _mapper.Map<BookResponse>(book);
                borrowedBooks.Add(bookResponse);
            }
        }

        if (!string.IsNullOrEmpty(searchTerm))
        {
            borrowedBooks = borrowedBooks.Where(b => b.Title.Contains(searchTerm)).ToList();
        }

        var totalCount = borrowedBooks.Count;

        var pagedBooks = borrowedBooks.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        return new PaginatedList<BookResponse>(pagedBooks, totalCount, pageNumber, pageSize);
    }

    public async Task<string> RequestBorrowAsync(Guid readerId, string email, List<Guid> bookIds)
    {
        if (bookIds == null || !bookIds.Any())
        {
            return "No books selected for borrowing.";
        }

        if (bookIds.Count > 5)
        {
            return "You can borrow maximum 5 books in 1 request";
        }

        var month = DateTime.Now.Month;

        var userRequestsThisMonth = await _requestRepository.GetRequestsByUserAndMonthAsync(readerId, month);

        if (userRequestsThisMonth.Count >= 3)
        {
            return "Limit of 3 borrowing requests per month exceeded.";
        }

        var checkedBookIds = new HashSet<Guid>();
        foreach (var bookId in bookIds)
        {
            if (!checkedBookIds.Add(bookId))
            {
                return "You cannot request the same book more than once in a single request.";
            }

            var isBookCurrentlyRequested = await _detailsRepository.IsBookCurrentlyRequested(bookId, readerId);
            if (isBookCurrentlyRequested == null) continue;
            var book = await _bookRepository.GetByIdAsync(bookId);
            if (book != null)
            {
                return "You have already requested for " + book.Title + " book. Please try again later.";
            }
        }

        var newRequest = new BookBorrowingRequest
        {
            RequestorId = readerId,
            DateRequested = DateTime.Now,
            Status = RequestStatus.Waiting,
            CreatedBy = email,
            CreatedAt = DateTime.Now,
            BookBorrowingRequestDetails = bookIds
                .Select(bookId => new BookBorrowingRequestDetails { BookId = bookId, })
                .ToList()
        };

        await _requestRepository.AddAsync(newRequest);
        return $"Request for {bookIds.Count} books has been submitted successfully.";
    }

    public async Task<bool> ManageBorrowingRequest(Guid librarianId, string email, Guid requestId, RequestStatus status)
    {
        var request = await _requestRepository.GetByIdAsync(requestId);
        if (request == null)
        {
            return false;
        }

        request.Status = status;
        request.ApproverId = librarianId;
        request.ModifyBy = email;
        request.ModifyAt = DateTime.Now;

        if (status == RequestStatus.Approved)
        {
            request.ExpiryDate = DateTime.Now.AddDays(14);
        }

        await _requestRepository.UpdateAsync(request);
        return true;
    }
}