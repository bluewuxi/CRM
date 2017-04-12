using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq.Expressions;

namespace CRM.WebUI.TagHelpers
{
    /// Paginated Meta data
    public class PaginatedMetaModel
    {
        public PaginatedMetaModel() { Pages = new List<Page>(); }
        public List<Page> Pages { get; set; }
        public PreviousPage PreviousPage { get; set; }
        public NextPage NextPage { get; set; }
    }
    public class PreviousPage
    {
        /// Do we need to display this node
        public bool Display { get; set; }

        /// Associated Page Number
        public int PageNumber { get; set; }
    }

    /// Next Node Meta
    public class NextPage
    {
        /// Do we need to display this node
        public bool Display { get; set; }
        /// Associated Page Number
        public int PageNumber { get; set; }
    }

    public class Page
    {
        /// Associated Page Number
        public int PageNumber { get; set; }

        /// Is this the current page
        public bool IsCurrent { get; set; }
    }

    [HtmlTargetElement("common-paging")]
    public class CommonPagingTagHelpers: TagHelper
    {
        /// Previous Node Meta

        public PaginatedMetaModel Info { get; set; }

        /// Base route minus page value
        public string Route { get; set; }
        public string Sorter { get; set; }
        public string Filter { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (Info == null) return;
            BuildParent(output);
            if (Info.PreviousPage.Display) AddPreviousPage(output);
            AddPageNodes(output);
            if (Info.NextPage.Display) AddNextPage(output);
        }

        /// Build parent tag (ul)
        private static void BuildParent(TagHelperOutput output)
        {
            output.TagName = "ul";
            output.Attributes.Add("class", "pagination");
            output.Attributes.Add("role", "navigation");
            output.Attributes.Add("aria-label", "Pagination");
        }

        /// Build previous page list item
        private void AddPreviousPage(TagHelperOutput output)
        {
            var html =$@"<li class=""pagination-previous""><a href=""{Route}/Page{Info.PreviousPage.PageNumber}?Sorter={Sorter}&Filter={Filter}"" aria-label=""Previous page""><i class=""glyphicon glyphicon-backward""></i></a></li>";
            output.Content.SetHtmlContent(output.Content.GetContent() + html);
        }

        /// Build next page list item
        private void AddNextPage(TagHelperOutput output)
        {
            var html =$@"<li class=""pagination-next""><a href=""{Route}/Page{Info.NextPage.PageNumber}?Sorter={Sorter}&Filter={Filter}"" aria-label=""Next page""><i class=""glyphicon glyphicon-forward""></i></a></li>";
            output.Content.SetHtmlContent(output.Content.GetContent() + html);
        }

        private void AddPageNodes(TagHelperOutput output)
        {
            foreach (var infoPage in Info.Pages)
            {
                string html;
                if (infoPage.IsCurrent)
                {
                    html = $@"<li><a class=""current"">{infoPage.PageNumber}</a></li>";
                    output.Content.SetHtmlContent(output.Content.GetContent() + html);
                    continue;
                }
                html = $@"<li><a href=""{Route}/Page{infoPage.PageNumber}?Sorter={Sorter}&Filter={Filter}"" aria-label=""Page {infoPage.PageNumber}"">{infoPage.PageNumber}</a></li>";
                output.Content.SetHtmlContent(output.Content.GetContent() + html);
            }
        }
    }
    //================================================================================================
    public interface IPaginatedMetaService
    {
        PaginatedMetaModel GetMetaData(int collectionSize, int selectedPageNumber, int itemsPerPage);
    }
    public class PaginatedMetaService : IPaginatedMetaService
    {
        /// <summary>
        /// As of current, we will always be displaying 5 options per pagination list.
        /// If this value changes at some point, some logic to calculate "middle" will break.
        /// </summary>
        private const int NumberOfNodesInPaginatedList = 5;

        /// <summary>
        /// Allows us to track indexes for previous and next 
        /// </summary>
        private List<Page> _pages;

        /// <summary>
        /// Builds meta data to be used with PioneerPaginationTagHelper
        /// </summary>
        /// <param name="collectionSize">Total size of collection we are paginating</param>
        /// <param name="selectedPageNumber">Selected page number of pagination</param>
        /// <param name="itemsPerPage">How many items per paginated list</param>

        public PaginatedMetaModel GetMetaData(int collectionSize, int selectedPageNumber, int itemsPerPage)
        {
            if (collectionSize <= 0) return null;
            _pages = BuildPageNodes(collectionSize, selectedPageNumber, itemsPerPage);
            return new PaginatedMetaModel
            {
                PreviousPage = BuildPreviousPage(collectionSize, selectedPageNumber, itemsPerPage),
                Pages = _pages,
                NextPage = BuildNextPage(collectionSize, selectedPageNumber, itemsPerPage)
            };
        }

        #region Previous Page
        /// <summary>
        /// Build previous page object
        /// </summary>
        private PreviousPage BuildPreviousPage(int collectionSize, int selectedPageNumber, int itemsPerPage)
        {
            var display = DisplayPreviousPage(collectionSize, selectedPageNumber, itemsPerPage);
            return new PreviousPage
            {
                Display = display,
                PageNumber = display ? _pages.First(x => x.IsCurrent).PageNumber - 1 : 1
            };
        }
        /// <summary>
        /// Determine if we need a Previous Page
        /// </summary>
        private bool DisplayPreviousPage(int collectionSize, int selectedPageNumber, int itemsPerPage)
        {
            return selectedPageNumber > 1 && collectionSize >= itemsPerPage;
        }
        /// <summary>
        /// Determine page number for previous node
        /// </summary>
        private int GetPreviousPageNumber(int selectedPageNumber)
        {
            return selectedPageNumber > 1 ? selectedPageNumber - 1 : 1;
        }
        #endregion

        #region Next Page
        /// <summary>
        /// Build next page object
        /// </summary>
        private NextPage BuildNextPage(int collectionSize, int selectedPageNumber, int itemsPerPage)
        {
            var display = DisplayNextPage(collectionSize, selectedPageNumber, itemsPerPage);
            return new NextPage
            {
                Display = display,
                PageNumber = display ? _pages.First(x => x.IsCurrent).PageNumber + 1 : NumberOfNodesInPaginatedList + 1
            };
        }

        /// <summary>
        /// Determine if we need a Next Page
        /// </summary>
        private bool DisplayNextPage(int collectionSize, int selectedPageNumber, int itemsPerPage)
        {
            return selectedPageNumber <= GetLastPageInCollection(collectionSize, itemsPerPage) - (NumberOfNodesInPaginatedList - 2);
        }

        #endregion

        #region Page Nodes
        /// <summary>
        /// Build individual page nodes
        /// </summary>
        private List<Page> BuildPageNodes(int collectionSize, int selectedPageNumber, int itemsPerPage)
        {
            var lastPage = GetLastPageInCollection(collectionSize, itemsPerPage);
            // If we have less then NumberOfNodesInPaginatedList * itemPage in our collectionSize,
            // then we know we have a partial list
            if (NumberOfNodesInPaginatedList * itemsPerPage > collectionSize)
            {
                return BuildPartialList(collectionSize, selectedPageNumber, itemsPerPage);
            }

            //  We are at the first collection of nodes in paginated list: 1, 2 & 3
            if (selectedPageNumber < NumberOfNodesInPaginatedList - 2)
            {
                return BuildStartList(selectedPageNumber);
            }
            // We are at the last collection of nodes in paginated list : last -2 , last - 1 & last
            if (selectedPageNumber > lastPage - (NumberOfNodesInPaginatedList - 2))
            {
                return BuildEndList(collectionSize, selectedPageNumber, itemsPerPage);
            }

            // We are at an in between collection of node in paginated list
            return BuildFullList(selectedPageNumber);
        }
        /// <summary>
        /// Build a full (NumberOfNodesInPaginatedList) collection of page nodes
        /// [ ][ ][ ][x][x][x][x][x][x][x][x][x][x][ ][ ][ ]
        /// </summary>
        private List<Page> BuildFullList(int selectedPageNumber)
        {
            var pages = new List<Page>
            {
                BuildNode(selectedPageNumber - 2),
                BuildNode(selectedPageNumber - 1),
                BuildNode(selectedPageNumber),
                BuildNode(selectedPageNumber + 1),
                BuildNode(selectedPageNumber + 2)
            };

            pages[2].IsCurrent = true;
            return pages;
        }
        /// <summary>
        /// Build a full (NumberOfNodesInPaginatedList) collection of page nodes
        /// [x][x][ ][ ][ ][ ][ ][ ][ ][ ] ][ ][ ][ ][ ][ ]
        /// </summary>
        private List<Page> BuildPartialList(int collectionSize, int selectedPageNumber, int itemsPerPage)
        {
            var pages = new List<Page>();
            for (var i = 0; i < GetLastPageInCollection(collectionSize, itemsPerPage); i++)
            {
                pages.Add(BuildNode(i + 1));
            }
            pages[selectedPageNumber - 1].IsCurrent = true;
            return pages;
        }
        /// <summary>
        /// Build list when selected page falls into first collection of node list
        /// Start shifting after three
        /// [*][*][*][x][x][ ][ ][ ][ ][ ][ ][ ][ ][ ][ ]
        /// </summary>
        private List<Page> BuildStartList(int selectedPageNumber)
        {
            var pages = new List<Page>
            {
                BuildNode(1),
                BuildNode(2),
                BuildNode(3),
                BuildNode(4),
                BuildNode(5)
            };
            pages[selectedPageNumber - 1].IsCurrent = true;
            return pages;
        }
        /// <summary>
        /// Build list when selected page falls into last collection of nodes
        /// Stop shifting after three from end
        /// [ ][ ][ ][ ][ ][ ][ ][ ][ ][ ][x][x][*][*][*]
        /// </summary>
        private List<Page> BuildEndList(int collectionSize, int selectedPageNumber, int itemsPerPage)
        {
            var lastPage = GetLastPageInCollection(collectionSize, itemsPerPage);
            var pages = new List<Page>
            {
                BuildNode(lastPage - 4),
                BuildNode(lastPage - 3),
                BuildNode(lastPage - 2),
                BuildNode(lastPage - 1),
                BuildNode(lastPage)
            };

            var unshiftedIndex = lastPage - selectedPageNumber;
            pages[NumberOfNodesInPaginatedList - 1 - unshiftedIndex].IsCurrent = true;
            return pages;
        }
        /// <summary>
        /// Build Selectable Node
        /// </summary>
        private Page BuildNode(int pageNumber)
        {
            return new Page
            {
                IsCurrent = false,
                PageNumber = pageNumber
            };
        }

        #endregion
        /// <summary>
        /// Get last page number in collection
        /// </summary>
        private int GetLastPageInCollection(int collectionSize, int itemsPerPage)
        {
            var lastPage = (double)collectionSize / itemsPerPage;
            // If whole number, we know nothing exist past lastPage
            if (lastPage % 1 == 0)
            {
                return (int)lastPage;
            }

            // If not whole number, we know there should be one more page past lastPage
            return (int)lastPage + 1;
        }

    }

    //===================================================================================
    /// <summary>
    /// Union ParameterExpression
    /// </summary>
    internal class ParameterReplacer : ExpressionVisitor
    {
        public ParameterReplacer(ParameterExpression paramExpr)
        {
            this.ParameterExpression = paramExpr;
        }

        public ParameterExpression ParameterExpression { get; private set; }

        public Expression Replace(Expression expr)
        {
            return this.Visit(expr);
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            return this.ParameterExpression;
        }
    }

    public static class PredicateExtensionses
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }

        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> exp_left, Expression<Func<T, bool>> exp_right)
        {
            var candidateExpr = Expression.Parameter(typeof(T), "candidate");
            var parameterReplacer = new ParameterReplacer(candidateExpr);

            var left = parameterReplacer.Replace(exp_left.Body);
            var right = parameterReplacer.Replace(exp_right.Body);
            var body = Expression.And(left, right);

            return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> exp_left, Expression<Func<T, bool>> exp_right)
        {
            var candidateExpr = Expression.Parameter(typeof(T), "candidate");
            var parameterReplacer = new ParameterReplacer(candidateExpr);

            var left = parameterReplacer.Replace(exp_left.Body);
            var right = parameterReplacer.Replace(exp_right.Body);
            var body = Expression.Or(left, right);

            return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
        }
    }
}
