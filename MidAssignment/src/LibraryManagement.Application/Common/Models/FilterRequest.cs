namespace LibraryManagement.Application.Common.Models;

public record FilterRequest
    (string? SearchTerm, string? SortColumn, string? SortOrder, int Page, int PageSize);