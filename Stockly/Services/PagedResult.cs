using System;

namespace Stockly.Services;

public class PagedResult<T> {
	public IEnumerable<T> Items { get; set; }
	public int TotalCount { get; set; }
	public int PageNumber { get; set; }
	public int PageSize { get; set; }

	public PagedResult(IEnumerable<T> items, int count, int pageNumber, int pageSize) {
		Items = items;
		TotalCount = count;
		PageNumber = pageNumber;
		PageSize = pageSize;
	}
}
