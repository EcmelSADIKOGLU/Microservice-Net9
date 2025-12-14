using Microservice_Net9_.Web.Options;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Microservice_Net9_.Web.TagHelpers
{
    public class CourseThumbnailPictureTagHelper(MicroserviceOption microserviceOption):TagHelper
    {
        public string? Src { get; set; }


        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "Image";

            var blankCourseThumbnailImagePath = "/images/blank_course.jpg";


            if (string.IsNullOrEmpty(Src))
            {
                output.Attributes.Add("src", blankCourseThumbnailImagePath);
            }
            else
            {
                var path = $"{microserviceOption.File.BaseAddress}/{Src}";

                output.Attributes.Add("src", path);
            }

                return base.ProcessAsync(context, output);
        }
    }
}
