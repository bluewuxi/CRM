using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CRM.WebUI.TagHelpers;
using System.Reflection;
using System.Linq.Expressions;

public class PaginatedList<T> : List<T>
{
    public PaginatedMetaModel PageModel;
    public string Filter = "";
    public string SortProperty = "";
    public bool Ascending = true;

    public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
    {
        PageModel = new PaginatedMetaService().GetMetaData(count, pageIndex, pageSize);
        this.AddRange(items);
    }

    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex=1, int pageSize=20, string filter="", string sortProperty="", bool ascending=true)
    {
        if (sortProperty!="" && sortProperty!=null) source=OrderBy(source, sortProperty, ascending);
        var count = await source.CountAsync();
        var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PaginatedList<T>(items, count, pageIndex, pageSize) { Filter = filter, SortProperty = sortProperty, Ascending=ascending};
    }

    public static IQueryable<T> OrderBy(IQueryable<T> source, string propertyName="", bool ascending=true) 
    {
        if (propertyName == "" || propertyName == null) return source;

        Type type = typeof(T);

        PropertyInfo property = type.GetProperty(propertyName);
        if (property == null)
            throw new ArgumentException("propertyName", "Not Exist");

        ParameterExpression param = Expression.Parameter(type, "p");
        Expression propertyAccessExpression = Expression.MakeMemberAccess(param, property);
        LambdaExpression orderByExpression = Expression.Lambda(propertyAccessExpression, param);

        string methodName = ascending ? "OrderBy" : "OrderByDescending";

        MethodCallExpression resultExp = Expression.Call(typeof(Queryable), methodName, new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExpression));

        return source.Provider.CreateQuery<T>(resultExp);
    }
}

