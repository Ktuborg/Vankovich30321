using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Vankovich30321.UI.MyTegHelpers
{
    [HtmlTargetElement("img", Attributes = "img-action, img-controller")]

    public class ImageTagHelper(LinkGenerator linkGenerator) : TagHelper
    {
        public string ImgController { get; set; }
        public string ImgAction { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.Add("src",
           linkGenerator.GetPathByAction(ImgAction, ImgController));

        }
    }
}
