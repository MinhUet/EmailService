#pragma checksum "D:\C#\Project\EmailService\BVMinh.EmailService.UI\Views\Miscellaneous\PasswordMeter.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0cecef1f26867ad7c674497c0c8b24f47476458b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Miscellaneous_PasswordMeter), @"mvc.1.0.view", @"/Views/Miscellaneous/PasswordMeter.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\C#\Project\EmailService\BVMinh.EmailService.UI\Views\_ViewImports.cshtml"
using BVMinh.EmailService.UI;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\C#\Project\EmailService\BVMinh.EmailService.UI\Views\_ViewImports.cshtml"
using BVMinh.EmailService.UI.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0cecef1f26867ad7c674497c0c8b24f47476458b", @"/Views/Miscellaneous/PasswordMeter.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"edf92a9916400e73ac9147bd91f124f70cfa9490", @"/Views/_ViewImports.cshtml")]
    public class Views_Miscellaneous_PasswordMeter : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("role", new global::Microsoft.AspNetCore.Html.HtmlString("form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/pwstrength/pwstrength-bootstrap.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/pwstrength/zxcvbn.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("names", "Development,Staging,Production", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.EnvironmentTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_EnvironmentTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "D:\C#\Project\EmailService\BVMinh.EmailService.UI\Views\Miscellaneous\PasswordMeter.cshtml"
  
    ViewData["Title"] = "PasswordMeter";

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"row wrapper border-bottom white-bg page-heading\">\r\n    <div class=\"col-lg-10\">\r\n        <h2>Password meter</h2>\r\n        <ol class=\"breadcrumb\">\r\n            <li class=\"breadcrumb-item\">\r\n                <a");
            BeginWriteAttribute("href", " href=\"", 267, "\"", 314, 1);
#nullable restore
#line 9 "D:\C#\Project\EmailService\BVMinh.EmailService.UI\Views\Miscellaneous\PasswordMeter.cshtml"
WriteAttributeValue("", 274, Url.Action("Dashboard_1", "Dashboards"), 274, 40, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@">Home</a>
            </li>
            <li class=""breadcrumb-item"">
                <a>Miscellaneous</a>
            </li>
            <li class=""active breadcrumb-item"">
                <strong>Password meter</strong>
            </li>
        </ol>
    </div>
    <div class=""col-lg-2"">

    </div>
</div>

<div class=""wrapper wrapper-content  animated fadeInRight"">
    <div class=""row"">
        <div class=""col-lg-12"">
            <div class=""ibox "">
                <div class=""ibox-title"">
                    <h5>Password meter</h5>
                </div>

                <div class=""ibox-content"">

                    <p class=""font-bold"">
                        Password Strength Meter for Twitter Bootstrap
                    </p>

                    <p>The jQuery Password Strength Meter is a plugin for Twitter Bootstrap that provides rulesets for visualy displaying the quality of a users typed in password.</p>


                </div>
            </div>
        </div>");
            WriteLiteral(@"
    </div>

    <div class=""row"">
        <div class=""col-md-6"">
            <div class=""ibox "">
                <div class=""ibox-title"">
                    <h5>Basic example </h5>
                </div>
                <div class=""ibox-content"">
                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0cecef1f26867ad7c674497c0c8b24f47476458b7228", async() => {
                WriteLiteral(@"
                        <div class=""row"" id=""pwd-container1"">
                            <div class=""col-sm-12"">
                                <div class=""form-group"">
                                    <label for=""password1"">Password</label>
                                    <input type=""password"" class=""form-control example1"" id=""password1"" placeholder=""Password"" value=""Passwo"">
                                </div>
                                <div class=""form-group"">
                                    <div class=""pwstrength_viewport_progress""></div>
                                </div>
                            </div>
                        </div>
                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                    <pre>
var options1 = {};
options1.ui = {
    container: ""#pwd-container"",
    showVerdictsInsideProgressBar: true,
    viewports: {
        progress: "".pwstrength_viewport_progress""
    }
};
options1.common = {
    debug: false
};
$('.example1').pwstrength(options1);
                            </pre>
                </div>
            </div>
        </div>
        <div class=""col-md-6"">
            <div class=""ibox "">
                <div class=""ibox-title"">
                    <h5>Show the password strength as a status in the field </h5>
                </div>
                <div class=""ibox-content"">
                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0cecef1f26867ad7c674497c0c8b24f47476458b9958", async() => {
                WriteLiteral(@"
                        <div class=""row"" id=""pwd-container2"">
                            <div class=""col-sm-12"">
                                <div class=""form-group"">
                                    <label for=""password2"">Password</label>
                                    <input type=""password"" class=""form-control example2"" id=""password2"" placeholder=""Password"" value=""Pass"">
                                </div>
                                <div class=""form-group"">
                                    <div class=""pwstrength_viewport_verdict""></div>
                                </div>
                            </div>
                        </div>
                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                    <pre>
var options2 = {};
options1.ui = {
    container: ""#pwd-container2"",
    showStatus: true,
    showProgressBar: false,
    viewports: {
        verdict: "".pwstrength_viewport_verdict""
    }
};
$('.example2').pwstrength(options2);
                            </pre>
                </div>
            </div>
        </div>
    </div>

    <div class=""row"">
        <div class=""col-md-6"">
            <div class=""ibox "">
                <div class=""ibox-title"">
                    <h5>Interact with other inputs  </h5>
                </div>
                <div class=""ibox-content"">
                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0cecef1f26867ad7c674497c0c8b24f47476458b12666", async() => {
                WriteLiteral(@"
                        <div class=""row"" id=""pwd-container3"">
                            <div class=""col-sm-12"">
                                <div class=""form-group"">
                                    <label for=""username"">Username</label>
                                    <input type=""text"" class=""form-control"" id=""username"" placeholder=""Username"">
                                </div>
                                <div class=""form-group"">
                                    <label for=""password3"">Password</label>
                                    <input type=""password"" class=""form-control example3"" id=""password3"" placeholder=""Password"">
                                </div>
                                <div class=""form-group"">
                                    <div class=""pwstrength_viewport_progress2""></div>
                                </div>
                            </div>
                        </div>
                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                    <pre>
var options3 = {};
options3.ui = {
    container: ""#pwd-container3"",
    showVerdictsInsideProgressBar: true,
    viewports: {
        progress: "".pwstrength_viewport_progress2""
    }
};
options3.common = {
    debug: true,
    usernameField: ""#username""
};
$('.example3').pwstrength(options3);
                            </pre>
                </div>
            </div>
        </div>
        <div class=""col-md-6"">
            <div class=""ibox "">
                <div class=""ibox-title"">
                    <h5>Use zxcvbn to calculate the password strength</h5>
                </div>
                <div class=""ibox-content"">
                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0cecef1f26867ad7c674497c0c8b24f47476458b15711", async() => {
                WriteLiteral(@"
                        <div class=""row"" id=""pwd-container4"">
                            <div class=""col-sm-12"">
                                <div class=""form-group"">
                                    <label for=""year"">Year of birth</label>
                                    <input type=""text"" class=""form-control"" id=""year"" value=""2016"">
                                </div>
                                <div class=""form-group"">
                                    <label for=""familyname"">Family Name</label>
                                    <input type=""text"" class=""form-control"" id=""familyname"" placeholder=""Name"" value=""Tokugawa"">
                                </div>
                                <div class=""form-group"">
                                    <label for=""password4"">Password</label>
                                    <input type=""password"" class=""form-control example4"" id=""password4"" placeholder=""Password"">
                                </div>
                 ");
                WriteLiteral(@"               <div class=""form-group"">
                                    <span class=""font-bold pwstrength_viewport_verdict4""></span>
                                    <span class=""pwstrength_viewport_progress4""></span>

                                    <p>The content of the form inputs and the words <em>samurai, shogun, bushido, daisho</em> and <em>seppuku</em> are disrecommended in the password, and the score will adjust properly.</p>
                                </div>
                            </div>
                        </div>
                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                    <pre>
var options4 = {};
options4.ui = {
    container: ""#pwd-container4"",
    viewports: {
        progress: "".pwstrength_viewport_progress4"",
        verdict: "".pwstrength_viewport_verdict4""
    }
};
options4.common = {
    zxcvbn: true,
    zxcvbnTerms: ['samurai', 'shogun', 'bushido', 'daisho', 'seppuku'],
    userInputs: ['#year', '#familyname']
};
$('.example4').pwstrength(options4);
                            </pre>
                </div>
            </div>
        </div>
    </div>
</div>

");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("environment", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0cecef1f26867ad7c674497c0c8b24f47476458b19364", async() => {
                    WriteLiteral("\r\n        ");
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0cecef1f26867ad7c674497c0c8b24f47476458b19646", async() => {
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    WriteLiteral("\r\n        ");
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0cecef1f26867ad7c674497c0c8b24f47476458b20806", async() => {
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    WriteLiteral("\r\n    ");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_EnvironmentTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.EnvironmentTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_EnvironmentTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_EnvironmentTagHelper.Names = (string)__tagHelperAttribute_3.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"

    <script type=""text/javascript"">
        $(document).ready(function () {

            // Example 1
            var options1 = {};
            options1.ui = {
                container: ""#pwd-container1"",
                showVerdictsInsideProgressBar: true,
                viewports: {
                    progress: "".pwstrength_viewport_progress""
                }
            };
            options1.common = {
                debug: false,
            };
            $('.example1').pwstrength(options1);

            // Example 2
            var options2 = {};
            options2.ui = {
                container: ""#pwd-container2"",
                showStatus: true,
                showProgressBar: false,
                viewports: {
                    verdict: "".pwstrength_viewport_verdict""
                }
            };
            $('.example2').pwstrength(options2);

            // Example 3
            var options3 = {};
            options3.ui = {
                c");
                WriteLiteral(@"ontainer: ""#pwd-container3"",
                showVerdictsInsideProgressBar: true,
                viewports: {
                    progress: "".pwstrength_viewport_progress2""
                }
            };
            options3.common = {
                debug: true,
                usernameField: ""#username""
            };
            $('.example3').pwstrength(options3);

            // Example 4
            var options4 = {};
            options4.ui = {
                container: ""#pwd-container4"",
                viewports: {
                    progress: "".pwstrength_viewport_progress4"",
                    verdict: "".pwstrength_viewport_verdict4""
                }
            };
            options4.common = {
                zxcvbn: true,
                zxcvbnTerms: ['samurai', 'shogun', 'bushido', 'daisho', 'seppuku'],
                userInputs: ['#year', '#familyname']
            };
            $('.example4').pwstrength(options4);

        });
    </script>
");
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
