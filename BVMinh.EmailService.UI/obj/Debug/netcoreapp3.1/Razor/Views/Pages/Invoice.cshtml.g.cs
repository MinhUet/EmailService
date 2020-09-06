#pragma checksum "D:\C#\Project\EmailService\BVMinh.EmailService.UI\Views\Pages\Invoice.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e515e5479c05228c35f4a7e2f7d3337c02f1ec6d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Pages_Invoice), @"mvc.1.0.view", @"/Views/Pages/Invoice.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e515e5479c05228c35f4a7e2f7d3337c02f1ec6d", @"/Views/Pages/Invoice.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"edf92a9916400e73ac9147bd91f124f70cfa9490", @"/Views/_ViewImports.cshtml")]
    public class Views_Pages_Invoice : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "D:\C#\Project\EmailService\BVMinh.EmailService.UI\Views\Pages\Invoice.cshtml"
  
    ViewData["Title"] = "Invoice";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"row wrapper border-bottom white-bg page-heading\">\r\n    <div class=\"col-lg-8\">\r\n        <h2>Invoice</h2>\r\n        <ol class=\"breadcrumb\">\r\n            <li class=\"breadcrumb-item\">\r\n                <a");
            BeginWriteAttribute("href", " href=\"", 257, "\"", 304, 1);
#nullable restore
#line 11 "D:\C#\Project\EmailService\BVMinh.EmailService.UI\Views\Pages\Invoice.cshtml"
WriteAttributeValue("", 264, Url.Action("Dashboard_1", "Dashboards"), 264, 40, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@">Home</a>
            </li>
            <li class=""breadcrumb-item"">
                Other Pages
            </li>
            <li class=""active breadcrumb-item"">
                <strong>Invoice</strong>
            </li>
        </ol>
    </div>
    <div class=""col-lg-4"">
        <div class=""title-action"">
            <a href=""#"" class=""btn btn-white""><i class=""fa fa-pencil""></i> Edit </a>
            <a href=""#"" class=""btn btn-white""><i class=""fa fa-check ""></i> Save </a>
            <a");
            BeginWriteAttribute("href", " href=\"", 812, "\"", 855, 1);
#nullable restore
#line 25 "D:\C#\Project\EmailService\BVMinh.EmailService.UI\Views\Pages\Invoice.cshtml"
WriteAttributeValue("", 819, Url.Action("invoicePrint", "Pages"), 819, 36, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" target=""_blank"" class=""btn btn-primary""><i class=""fa fa-print""></i> Print Invoice </a>
        </div>
    </div>
</div>
<div class=""wrapper wrapper-content animated fadeInRight"">
    <div class=""ibox-content p-xl"">
        <div class=""row"">
            <div class=""col-sm-6"">
                <h5>From:</h5>
                <address>
                    <strong>Inspinia, Inc.</strong><br>
                    106 Jorg Avenu, 600/10<br>
                    Chicago, VT 32456<br>
                    <abbr title=""Phone"">P:</abbr> (123) 601-4590
                </address>
            </div>

            <div class=""col-sm-6 text-right"">
                <h4>Invoice No.</h4>
                <h4 class=""text-navy"">INV-000567F7-00</h4>
                <span>To:</span>
                <address>
                    <strong>Corporate, Inc.</strong><br>
                    112 Street Avenu, 1080<br>
                    Miami, CT 445611<br>
                    <abbr title=""Phone"">P:</abbr> (120) 9000-4");
            WriteLiteral(@"321
                </address>
                <p>
                    <span><strong>Invoice Date:</strong> Marh 18, 2014</span><br />
                    <span><strong>Due Date:</strong> March 24, 2014</span>
                </p>
            </div>
        </div>

        <div class=""table-responsive m-t"">
            <table class=""table invoice-table"">
                <thead>
                    <tr>
                        <th>Item List</th>
                        <th>Quantity</th>
                        <th>Unit Price</th>
                        <th>Tax</th>
                        <th>Total Price</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>
                            <div><strong>Admin Theme with psd project layouts</strong></div>
                            <small>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</");
            WriteLiteral(@"small>
                        </td>
                        <td>1</td>
                        <td>$26.00</td>
                        <td>$5.98</td>
                        <td>$31,98</td>
                    </tr>
                    <tr>
                        <td>
                            <div><strong>Wodpress Them customization</strong></div>
                            <small>
                                Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.
                                Eiusmod tempor incididunt ut labore et dolore magna aliqua.
                            </small>
                        </td>
                        <td>2</td>
                        <td>$80.00</td>
                        <td>$36.80</td>
                        <td>$196.80</td>
                    </tr>
                    <tr>
                        <td>
                            <div><strong>Angular JS & Node ");
            WriteLiteral(@"JS Application</strong></div>
                            <small>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</small>
                        </td>
                        <td>3</td>
                        <td>$420.00</td>
                        <td>$193.20</td>
                        <td>$1033.20</td>
                    </tr>

                </tbody>
            </table>
        </div><!-- /table-responsive -->

        <table class=""table invoice-total"">
            <tbody>
                <tr>
                    <td><strong>Sub Total :</strong></td>
                    <td>$1026.00</td>
                </tr>
                <tr>
                    <td><strong>TAX :</strong></td>
                    <td>$235.98</td>
                </tr>
                <tr>
                    <td><strong>TOTAL :</strong></td>
                    <td>$1261.98</td>
                </tr>
            </tbody>
   ");
            WriteLiteral(@"     </table>
        <div class=""text-right"">
            <button class=""btn btn-primary""><i class=""fa fa-dollar""></i> Make A Payment</button>
        </div>

        <div class=""well m-t"">
            <strong>Comments</strong>
            It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less
        </div>
    </div>
</div>

");
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
