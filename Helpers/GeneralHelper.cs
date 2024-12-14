using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using Solvi.Resources;

namespace Solvi.Helpers
{
    public static class GeneralHelper
    {

        public static IHtmlContent CustomDropDown(string? inputName, List<SelectListItem>? selectList, bool searchBar = true, string? onChangeValueJsMethod = "", string? placeholder = "", string? selectedValue = "", string? extraclass = "", bool? removeHtml = false)
        {
            var div = new TagBuilder("div");
            div.AddCssClass("form-dropdown dropdown");
            div.GenerateId($"{inputName}-ddl", "_");

            var input = new TagBuilder("input");
            input.AddCssClass("dropdown-input");
            input.Attributes.Add("id", inputName);
            input.Attributes.Add("name", inputName);
            selectList = selectList == null ? new List<SelectListItem>() : selectList;
            selectedValue = (string.IsNullOrEmpty(selectedValue) ?
                selectList.FirstOrDefault(a => a?.Selected == true)?.Value : selectedValue) ?? "";
            if (!string.IsNullOrEmpty(selectedValue))
            {
                input.Attributes.Add("value", selectedValue);
            }
            input.Attributes.Add("hidden", "");

            var button = new TagBuilder("button");
            button.AddCssClass($"btn form-control dropdown-toggle w-100 rounded-xl text-start border-1 ws-breakspaces d-flex justify-content-between align-items-center {extraclass}");
            button.Attributes.Add("type", "button");
            button.Attributes.Add("data-bs-toggle", "dropdown");
            button.Attributes.Add("aria-expanded", "false");

            selectList = selectList == null ? new List<SelectListItem>() : selectList;
            string selectedText = (string.IsNullOrEmpty(selectedValue) ?
                selectList.FirstOrDefault(a => a?.Selected == true)?.Text :
                selectList.FirstOrDefault(a => a?.Value == selectedValue)?.Text) ?? "";
            string placeholderText = string.IsNullOrEmpty(placeholder) ?
                "Please select" : placeholder;
            string appendText = string.IsNullOrEmpty(selectedText) ? placeholderText : selectedText;
            button.Attributes.Add("placeholder", appendText);
            button.InnerHtml.Append(appendText);

            var ul = new TagBuilder("ul");
            ul.AddCssClass("dropdown-menu w-100");

            if (searchBar)
            {
                var searchLi = new TagBuilder("li");

                var inputGroup = new TagBuilder("div");
                inputGroup.AddCssClass("input-group");

                var inputGroupIcon = new TagBuilder("span");
                inputGroupIcon.AddCssClass("input-group-text");
                inputGroupIcon.InnerHtml.AppendHtml("<i class=\"bi bi-search\"></i>");

                var searchInput = new TagBuilder("input");
                searchInput.AddCssClass("form-control search");
                searchInput.Attributes.Add("type", "text");
                searchInput.Attributes.Add("autofocus", "autofocus");
                searchInput.Attributes.Add("placeholder", "Search...");
                searchInput.Attributes.Add("aria-label", "Search");

                inputGroup.InnerHtml.AppendHtml(inputGroupIcon);
                inputGroup.InnerHtml.AppendHtml(searchInput);

                searchLi.InnerHtml.AppendHtml(inputGroup);

                ul.InnerHtml.AppendHtml(searchLi);
            }

            if (selectList != null)
            {
                int index = 1;
                foreach (var item in selectList)
                {
                    var li = new TagBuilder("li");
                    li.AddCssClass("dropdown-list");
                    li.Attributes.Add("data-index", index.ToString());
                    var a = new TagBuilder("a");
                    a.Attributes.Add("data-value", item.Value);
                    if (!string.IsNullOrEmpty(selectedValue) && selectedValue == item.Value)
                    {
                        a.AddCssClass("dropdown-item active");
                    }
                    else if (item.Selected == true && string.IsNullOrEmpty(selectedValue))
                    {
                        a.AddCssClass("dropdown-item active");
                    }
                    else
                    {
                        a.AddCssClass("dropdown-item");
                    }
                    if (!string.IsNullOrEmpty(onChangeValueJsMethod))
                    {
                        a.Attributes.Add("onclick", onChangeValueJsMethod);
                    }

                    a.InnerHtml.Append(removeHtml == true ? GeneralHelper.RemoveHtml(item.Text) : item.Text);
                    li.InnerHtml.AppendHtml(a);
                    ul.InnerHtml.AppendHtml(li);
                    index++;
                }
            }

            div.InnerHtml.AppendHtml(input);
            div.InnerHtml.AppendHtml(button);
            div.InnerHtml.AppendHtml(ul);

            return div;
        }

        public static string ReplaceWords(string sentence, string target, string replaceWith)
        {
            string result = sentence.Replace(target, replaceWith);
            return result;
        }

        public static void SanitizeModel<T>(T model) where T : class
        {
            if (model != null)
            {
                var properties = model.GetType().GetProperties();
                foreach (var property in properties)
                {
                    if (property.PropertyType == typeof(string))
                    {
                        var value = (string?)property.GetValue(model);
                        if (!string.IsNullOrEmpty(value))
                        {
                            var sanitizedValue = SanitizeHtmlAndJavaScript(value);
                            property.SetValue(model, sanitizedValue);
                        }
                    }
                }
            }
        }

        public static string SanitizeHtmlAndJavaScript(string value)
        {
            var htmlRegex = new Regex(@"<[^>]+>");
            var scriptRegex = new Regex(@"<script[^>]*>[\s\S]*?</script>");

            // Encode HTML tags
            value = htmlRegex.Replace(value, match => WebUtility.HtmlEncode(match.Value));

            // Encode JavaScript code, excluding quotation marks, "&#x27;", "&#xA;", and newline characters
            value = scriptRegex.Replace(value, match =>
            {
                var encodedMatch = WebUtility.HtmlEncode(match.Value);
                encodedMatch = encodedMatch.Replace("&quot;", "\"");
                encodedMatch = encodedMatch.Replace("&#x27;", "'");
                encodedMatch = encodedMatch.Replace("&#xA;", "\n");
                encodedMatch = encodedMatch.Replace("\\n", "\n");
                return encodedMatch;
            });

            return value;
        }

        public static string RemoveHtml(string? input, bool? excludeBrTag = false)
        {
            if (!string.IsNullOrEmpty(input))
            {
                input = input.Replace("&lt;", "<");
                input = input.Replace("&gt;", ">");
                if (excludeBrTag == true)
                {
                    return Regex.Replace(input, "<(?!br\\s*>)\\s*.*?>", string.Empty);
                }
                else
                {
                    input = input.Replace("<br>", " ");
                }
                return Regex.Replace(input, "<.*?>", string.Empty);
            }
            return "";
        }

        public static string CapitalizeEachWord(string? input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                string result = textInfo.ToTitleCase(input.ToLower());
                return result;
            }
            return "";
        }

    }
}
