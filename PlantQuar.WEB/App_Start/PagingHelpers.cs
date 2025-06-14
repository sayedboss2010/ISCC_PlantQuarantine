////using Microsoft.AspNetCore.Html;
////using Microsoft.AspNetCore.Mvc.Rendering;
////using System;
////using System.Globalization;

////namespace PlantQuar.WEB.HtmlHelpers
////{
////    public static class PagingHelpers
////    {
////        public static string pageUrl(int curPage)
////        {
////            return "?page=" + curPage.ToString();
////        }


////        public static string pageUrlSearch(int curPage, string Search)
////        {
////            return "?page=" + curPage.ToString() + "&Search=" + Search;
////        }
////        /// <summary>
////        /// This is applicable for Bootstrap version 3.0
////        /// </summary>
////        /// <param name="html"></param>
////        /// <param name="CurrentPage"></param>
////        /// <param name="TotalPages"></param>
////        /// <param name="pageUrl"></param>
////        /// <returns></returns>
////        /// 
////        public static IHtmlContent BootstrapPageLinks(int CurrentPage, int TotalPages, int TotalResults,string Search)
////        {
////            bool isRtl = CultureInfo.CurrentCulture.Name == "ar-EG";
////            string records = isRtl ? "سجلات" : "records";
////            string nextStr = isRtl ? "التالى" : "Next";
////            string previousStr = isRtl ? "السابق" : "Previous";
////            string firstPage = isRtl ? "أول صفحة" : "First Page";
////            string lastPage = isRtl ? "أخر صفحة" : "Last Page";



////            //number of pages to be displayed
////            const short max = 10;
////            double level = Math.Ceiling(CurrentPage / (double)max) * max;

////            var list = new TagBuilder("ul");
////            if (isRtl)
////                list.MergeAttribute("direction", "rtl");

////            list.MergeAttribute("class", "pagination");

////            int startPage = (int)level - max + 1;

////            IHtmlContent TagMaker(string text, string url, bool isActive)
////            {
////                var pageNumberTag = new TagBuilder("a");
////                pageNumberTag.MergeAttribute("class", "page-link");

////                if (!string.IsNullOrWhiteSpace(url))
////                    pageNumberTag.MergeAttribute("href", url);

////                pageNumberTag.InnerHtml.AppendHtml(new HtmlString(text));

////                var listItem = new TagBuilder("li");
////                list.MergeAttribute("class", "page-item");

////                if (isActive)
////                    listItem.MergeAttribute("class", "active", replaceExisting: false);

////                listItem.InnerHtml.AppendHtml(pageNumberTag);
////                return listItem;
////            }

////            IHtmlContent DirectionMaker(string destination, string url, bool isLink = true)
////            {
////                var liChildTag = new TagBuilder("a");
////                liChildTag.MergeAttribute("class", "page-link");

////                if (isLink)
////                {
////                    if (!string.IsNullOrWhiteSpace(url))
////                        liChildTag.MergeAttribute("href", url);
////                }
////                else
////                {
////                    liChildTag.MergeAttribute("class", "page-link disabled", true);
////                }

////                var iconTag = new TagBuilder("span");
////                string liClasses = "";
////                if (!string.IsNullOrWhiteSpace(destination))
////                {
////                    iconTag.InnerHtml.AppendHtml(destination);

////                    if (destination == nextStr)
////                        liClasses += "next";

////                    if (destination == previousStr)
////                        liClasses += "prev";
////                }

////                liChildTag.InnerHtml.AppendHtml(iconTag);

////                var listItem = new TagBuilder("li");
////                listItem.MergeAttribute("class", liClasses, true);
////                listItem.InnerHtml.AppendHtml(liChildTag);
////                return listItem;
////            }

////            if (TotalPages > 1 && CurrentPage != 1)
////            {
////                //if (isRtl)
////                //{
////                list.InnerHtml.AppendHtml(DirectionMaker(previousStr, pageUrlSearch(CurrentPage - 1, Search)));
////                list.InnerHtml.AppendHtml(DirectionMaker(firstPage, pageUrlSearch(1, Search)));
////                //}
////                //else
////                //{
////                //    list.InnerHtml.AppendHtml(DirectionMaker("fa fa-angle-double-left", pageUrl(1)));
////                //    list.InnerHtml.AppendHtml(DirectionMaker("fa fa-angle-left", pageUrl(CurrentPage - 1)));
////                //}
////            }
////            else
////            {
////                list.InnerHtml.AppendHtml(DirectionMaker(previousStr, pageUrlSearch(CurrentPage - 1, Search), false));
////            }

////            for (int i = startPage; i <= level; i++)
////            {
////                if (i > TotalPages)
////                    break;

////                if (i == CurrentPage)
////                    list.InnerHtml.AppendHtml(TagMaker(i + "", "", true));
////                else
////                    list.InnerHtml.AppendHtml(TagMaker(i + "", pageUrlSearch(i,Search), false));
////            }

////            if (TotalPages > 1 && CurrentPage != TotalPages)
////            {
////                //if (isRtl)
////                //{
////                list.InnerHtml.AppendHtml(DirectionMaker(lastPage, pageUrlSearch(TotalPages, Search)));

////                if (TotalResults > 1)
////                    list.InnerHtml.AppendHtml(TagMaker($"{TotalResults} {records}", null, false));

////                list.InnerHtml.AppendHtml(DirectionMaker(nextStr, pageUrlSearch(CurrentPage + 1, Search)));
////                //}
////                //else
////                //{
////                //    list.InnerHtml.AppendHtml(DirectionMaker("fa fa-angle-right", pageUrl(CurrentPage + 1)));
////                //    list.InnerHtml.AppendHtml(DirectionMaker("fa fa-angle-double-right", pageUrl(TotalPages)));
////                //}
////            }
////            else
////            {
////                if (TotalResults > 1)
////                    list.InnerHtml.AppendHtml(TagMaker($"{TotalResults} {records}", null, false));

////                list.InnerHtml.AppendHtml(DirectionMaker(nextStr, pageUrlSearch(CurrentPage + 1, Search), false));
////            }


////            if (TotalPages > 1)
////            {
////                //direct page navigation
////                var boxTag = new TagBuilder("input");
////                boxTag.MergeAttribute("placeholder", "page");
////                boxTag.MergeAttribute("type", "text");
////                boxTag.MergeAttribute("style", "width:60px;margin:0;");
////                //long nasty javascript call. Be careful when modifying this.
////                string jsNavigate = "if (event.keyCode==13){" +
////                                 "if(isNaN(this.value) || this.value < 1){alert('Please enter positive number');return;}" +
////                                 "if(this.value > " + TotalPages + "){alert('Maximum number allowed is " + TotalPages +
////                                 "');return;}" +
////                                 "var url ='" + pageUrlSearch(1, Search).Replace("page=1", "page=") + "';" + //REMEMBER that page= must be all lowercase otherwise replacement won't work
////                                 "url = url.replace('page=', 'page=' + this.value);" +
////                                 "window.location = url;" +
////                                 "}";

////                boxTag.MergeAttribute("onkeypress", jsNavigate);

////                var listItem = new TagBuilder("li");
////                listItem.MergeAttribute("class", "page-item");

////                var wrapper = new TagBuilder("a");
////                wrapper.MergeAttribute("class", "page-link");
////                wrapper.InnerHtml.AppendHtml(boxTag);
////                wrapper.MergeAttribute("style", "padding-top:9px;padding-bottom:9px;");
////                listItem.InnerHtml.AppendHtml(wrapper);
////            }

////            var result = new TagBuilder("nav");
////            result.InnerHtml.AppendHtml(list);
////            return result;
////        }
////    }

////}
//using System.Web.Mvc;
//namespace PlantQuar.WEB.HtmlHelpers
//{
//    public static class PagingHelpers
//    {
//        public static string pageUrlSearch(int curPage, string Search)
//        {
//            return "?page=" + curPage.ToString() + "&Search=" + Search;
//        }
//        // دالة لتوليد عناصر الباجنيشن
//        public static MvcHtmlString BootstrapPageLinks(this HtmlHelper html, int currentPage, int totalPages, string search = "")
//        {
//            // إنشاء عنصر الـ <ul> الخاص بالقائمة
//            var ul = new TagBuilder("ul");
//            ul.AddCssClass("pagination");

//            // زر السابق
//            if (currentPage > 1)
//            {

//                var prevLi = new TagBuilder("li");
//                prevLi.AddCssClass("page-item");
//                var prevA = new TagBuilder("a");
//                prevA.AddCssClass("page-link");
//                //prevA.Attributes["href"] = $"/Export_CheckRequest/List_EXCheckRequest/Index_ListAll?CurrentPage={currentPage - 1}&Search={search}";
//                prevA.Attributes["href"] = pageUrlSearch(currentPage - 1, search);
//                    //$"/Export_CheckRequest/List_EXCheckRequest/Index_ListAll?CurrentPage={currentPage - 1}&Search={search}";
//                prevA.SetInnerText("« السابق");
//                prevLi.InnerHtml = prevA.ToString();
//                ul.InnerHtml += prevLi.ToString();
//            }

//            // إضافة الصفحات (توليد الروابط الخاصة بالصفحات)
//            for (int i = 1; i <= totalPages; i++)
//            {
//                var li = new TagBuilder("li");
//                li.AddCssClass("page-item");

//                if (i == currentPage)
//                {
//                    li.AddCssClass("active");  // تحديد الصفحة الحالية
//                }

//                var a = new TagBuilder("a");
//                a.AddCssClass("page-link");
//                //a.Attributes["href"] = $"/Export_CheckRequest/Index?page={i}&search={search}";
//                //a.Attributes["href"] = $"/Export_CheckRequest/List_EXCheckRequest/Index_ListAll?CurrentPage={i}&Search={search}";
//                a.Attributes["href"] = pageUrlSearch(i, search);

//                a.SetInnerText(i.ToString());

//                li.InnerHtml = a.ToString();
//                ul.InnerHtml += li.ToString();
//            }

//            // زر التالي
//            if (currentPage < totalPages)
//            {
//                var nextLi = new TagBuilder("li");
//                nextLi.AddCssClass("page-item");
//                var nextA = new TagBuilder("a");
//                nextA.AddCssClass("page-link");
//                //nextA.Attributes["href"] = $"/Export_CheckRequest/List_EXCheckRequest/Index_ListAll?CurrentPage={currentPage + 1}&Search={search}";
//                nextA.Attributes["href"] = pageUrlSearch(currentPage + 1, search);

//                nextA.SetInnerText("التالي »");
//                nextLi.InnerHtml = nextA.ToString();
//                ul.InnerHtml += nextLi.ToString();
//            }

//            // إرجاع HTML الخاص بالقائمة
//            return MvcHtmlString.Create(ul.ToString());
//        }
//    }
//}

using System.Web.Mvc;

namespace PlantQuar.WEB.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static string pageUrlSearch(int curPage, string search)
        {
            return "?CurrentPage=" + curPage + "&Search=" + search;
        }

        public static MvcHtmlString BootstrapPageLinks(this HtmlHelper html, int currentPage, int totalPages, string search = "")
        {
            var ul = new TagBuilder("ul");
            ul.AddCssClass("pagination");

            int pageWindow = 5;

            int startPage = currentPage - pageWindow;
            int endPage = currentPage + pageWindow;

            if (startPage < 1)
            {
                endPage += 1 - startPage;
                startPage = 1;
            }

            if (endPage > totalPages)
            {
                startPage -= endPage - totalPages;
                endPage = totalPages;
                if (startPage < 1) startPage = 1;
            }

            // زر "السابق"
            if (currentPage > 1)
                ul.InnerHtml += BuildPageItem(currentPage - 1, "« السابق", search);
            else
                ul.InnerHtml += BuildDisabledItem("« السابق");

            // زر أول صفحة + نقاط
            if (startPage > 1)
            {
                ul.InnerHtml += BuildPageItem(1, "1", search);
                if (startPage > 2)
                    ul.InnerHtml += BuildDisabledItem("...");
            }

            // الصفحات
            for (int i = startPage; i <= endPage; i++)
            {
                bool isActive = i == currentPage;
                ul.InnerHtml += BuildPageItem(i, i.ToString(), search, isActive);
            }

            // زر آخر صفحة + نقاط
            if (endPage < totalPages)
            {
                if (endPage < totalPages - 1)
                    ul.InnerHtml += BuildDisabledItem("...");
                ul.InnerHtml += BuildPageItem(totalPages, totalPages.ToString(), search);
            }

            // زر "التالي"
            if (currentPage < totalPages)
                ul.InnerHtml += BuildPageItem(currentPage + 1, "التالي »", search);
            else
                ul.InnerHtml += BuildDisabledItem("التالي »");

            return MvcHtmlString.Create(ul.ToString());
        }

        private static string BuildPageItem(int page, string text, string search, bool isActive = false)
        {
            var li = new TagBuilder("li");
            li.AddCssClass("page-item");
            if (isActive)
                li.AddCssClass("active");

            var a = new TagBuilder("a");
            a.AddCssClass("page-link");
            a.Attributes["href"] = pageUrlSearch(page, search);
            a.SetInnerText(text);

            li.InnerHtml = a.ToString();
            return li.ToString();
        }

        private static string BuildDisabledItem(string text)
        {
            var li = new TagBuilder("li");
            li.AddCssClass("page-item disabled");

            var span = new TagBuilder("span");
            span.AddCssClass("page-link");
            span.SetInnerText(text);

            li.InnerHtml = span.ToString();
            return li.ToString();
        }

    }
}
