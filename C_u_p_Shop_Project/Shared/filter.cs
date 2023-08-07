using C_u_p_Shop_Project.Models;

namespace C_u_p_Shop_Project.Shared
{
    public static class filter
    {
        public static List<Product> sorted_Products(IQueryable<Product> products, string sort, int skip, int limit)
        {
            switch (sort)
            {
                case "Newest":
                    return products.OrderByDescending(p => p.registerDate).Skip(skip).Take(limit).ToList();
                case "Oldest":
                    return products.OrderBy(p => p.registerDate).Skip(skip).Take(limit).ToList();
                case "ExpensiveToCheap":
                    return products.OrderByDescending(p => p.Price).Skip(skip).Take(limit).ToList();
                case "CheapToExpensive":
                    return products.OrderBy(p => p.Price).Skip(skip).Take(limit).ToList();
                case "AlphabetAscending":
                    return products.OrderBy(p => p.Name).Skip(skip).Take(limit).ToList();
                case "AlphabetDescending":
                    return products.OrderByDescending(p => p.Name).Skip(skip).Take(limit).ToList();
                default:
                    return products.Skip(skip).Take(limit).ToList();
            }
        }
    }
}
