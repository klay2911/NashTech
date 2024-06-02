using AutoMapper;
using LibraryManagement.Application.Common.Models;
using LibraryManagement.Application.DTOs.BookDTOs;
using LibraryManagement.Application.DTOs.BorrowingRequestDTOs;
using LibraryManagement.Application.Interfaces.Repositories;
using LibraryManagement.Application.Interfaces.Services;
using LibraryManagement.Domain.Enum;
using LibraryManagement.Domain.Models;

namespace LibraryManagement.Infrastructure.Services;

public class BookBorrowingRequestService : IBookBorrowingRequestService
{
    private readonly IBookBorrowingRequestRepository _requestRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public BookBorrowingRequestService(IBookBorrowingRequestRepository requestRepository, IBookRepository bookRepository, IMapper mapper)
    {
        _requestRepository = requestRepository;
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<BorrowingRequestResponse>> GetWaitingRequests(int pageNumber, int pageSize, string searchTerm = "")
    {
        var requests = await _requestRepository.GetAllAsync();
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
        
        response.Books = request.BookBorrowingRequestDetails
        .Select(detail => _mapper.Map<BookResponse>(detail.Book))
        .ToList();
        
        return response;
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