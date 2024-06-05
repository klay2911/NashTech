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

    public BookBorrowingService(IBookBorrowingRequestRepository requestRepository, IBookRepository bookRepository,IBookBorrowingRequestDetailsRepository detailsRepository, IMapper mapper)
    {
        _requestRepository = requestRepository;
        _bookRepository = bookRepository;
        _detailsRepository = detailsRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<BorrowingRequestResponse>> GetWaitingRequests(int pageNumber, int pageSize, string searchTerm = "")
    {
        var requests = await _requestRepository.GetAllWithUserAsync();
        var requestsQuery = requests.Where(r => r.Status == RequestStatus.Waiting);
        
        if (!string.IsNullOrEmpty(searchTerm))
        {
            requestsQuery = requestsQuery.Where(r => r.User.FirstName.Contains(searchTerm) || r.User.LastName.Contains(searchTerm));
        }

        var paginatedRequests =  PaginatedList<BookBorrowingRequest>.Create(requestsQuery, pageNumber, pageSize);

        var mappedRequests = paginatedRequests.Items.Select(r => _mapper.Map<BorrowingRequestResponse>(r)).ToList();

        return new PaginatedList<BorrowingRequestResponse>(mappedRequests, paginatedRequests.TotalCount, pageNumber, pageSize);
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
    
    public async Task<PaginatedList<BookResponse>> GetUserBorrowedBooks(Guid userId, int pageNumber, int pageSize, string searchTerm = "")
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
    // public async Task<IPagedList<BookViewModel>> GetUserBorrowedBooks(int userId, int pageNumber, int pageSize, string searchTerm)
    // {
    //     var borrowingRequests = await _unitOfWork.BookBorrowingRequestRepository.GetRequestsByUser(userId);
    //
    //     var borrowedBooks = new List<BookViewModel>();
    //
    //     foreach (var request in borrowingRequests)
    //     {
    //         foreach (var detail in request.BookBorrowingRequestDetails)
    //         {
    //             var book = await _unitOfWork.BookRepository.GetByIdAsync(detail.BookId);
    //             var bookViewModel = new BookViewModel
    //             {
    //                 BookId = book.BookId,
    //                 Title = book.Title,
    //                 Author = book.Author,
    //                 ISBN = book.ISBN,
    //                 CategoryId = book.CategoryId,
    //                 Status = request.Status,
    //                 ExpiryDate = request.ExpiryDate,
    //                 PdfFilePath = book.PdfFilePath,
    //                 LastReadPageNumber = detail.LastReadPageNumber
    //             };
    //             if (request.Status == "Approved")
    //             {
    //                 request.ExpiryDate = DateTime.Now.AddDays(10);
    //                 _unitOfWork.BookBorrowingRequestRepository.Update(request);
    //                 await _unitOfWork.SaveAsync();
    //             }
    //             borrowedBooks.Add(bookViewModel);
    //         }
    //     }
    //     borrowedBooks = borrowedBooks.OrderByDescending(b => b.ExpiryDate).ToList();
    //     if (!string.IsNullOrEmpty(searchTerm))
    //     {
    //         borrowedBooks = borrowedBooks.Where(b => b.Title.Contains(searchTerm)).ToList();
    //     }
    //
    //     int totalCount = borrowedBooks.Count();
    //
    //     var pagedBooks = borrowedBooks.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
    //
    //     return new StaticPagedList<BookViewModel>(pagedBooks, pageNumber, pageSize, totalCount);
    // }
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

        var userRequestsThisMonth = await _requestRepository.GetRequestsByUserAndMonthAsync(
            readerId,
            month
        );

        if (userRequestsThisMonth.Count >= 3)
        {
            return "Limit of 3 borrowing requests per month exceeded.";
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
            //request.ApproverId = librarianId;
            request.ExpiryDate = DateTime.Now.AddDays(14);
        }

        await _requestRepository.UpdateAsync(request);
        return true;
    }

}