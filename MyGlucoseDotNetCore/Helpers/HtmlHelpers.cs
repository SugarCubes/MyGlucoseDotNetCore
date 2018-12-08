using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyGlucoseDotNetCore.Helpers
{
    public static class HtmlHelpers
    {
        public static IHtmlContent PatientChartNavigationDropDown( this IHtmlHelper htmlHelper, string UserName, string classes = null )
            => new HtmlString( "<div class=\"" + classes + "\">" +
            "\n    <select onchange=\"location=this.value;\" class=\"form-control\">" +
            "\n        <option value=\"\">-- Select a Graph to View --</option>" +
            "\n        <option value=\"/Chart/GlucoseIndex?UserName="+ UserName + "\">Glucose Graph</option>" +
            "\n        <option value=\"/Chart/ExerciseIndex?UserName=" + UserName + "\">Exercise Graph</option>" +
            "\n        <option value=\"/Chart/MealIndex?UserName=" + UserName + "\">Meal Graph</option>" +
            "\n        <option value=\"/Chart/StepIndex?UserName=" + UserName + "\">Step Graph</option>" +
            "\n    </select> " +
            "\n</div>" );

    } // class

} //namespace
