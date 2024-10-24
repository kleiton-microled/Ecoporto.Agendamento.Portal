using System.Web;
using System.Web.Optimization;

namespace Ecoporto.Agendamento.Portal
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery")
                     .Include("~/Content/js/jquery-1.12.4.js",
                              "~/Content/js/jquery-ui-1.12.1.js",
                              "~/Content/js/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-url")
               .Include("~/Content/plugins/jquery-url/jquery-url.js"));

            bundles.Add(new ScriptBundle("~/bundles/site")
               .Include("~/Content/plugins/easyAutocomplete/jquery.easy-autocomplete.js",
                        "~/Content/js/default.js",
                        "~/Content/js/mensagens-redirecionamento.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-validate")
                .Include("~/Content/plugins/jquery-validate/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-mask")
                .Include("~/Content/plugins/jquery-mask/jquery.mask.js"));

            bundles.Add(new StyleBundle("~/bundles/css")
                .Include("~/Content/css/bootstrap.css",
                         "~/Content/css/estilos.css",
                         "~/Content/css/fontawesome-all.css",
                         "~/Content/plugins/toastr/toastr.css",
                         "~/Content/css/jquery-ui.css",
                         "~/Content/plugins/easyAutocomplete/easy-autocomplete.css"));

            bundles.Add(new StyleBundle("~/bundles/login")
               .Include("~/Content/css/bootstrap.css",
                        "~/Content/css/tooltip.css",
                        "~/Content/css/fontawesome-all.css",
                        "~/Content/css/login.css"));

            bundles.Add(new StyleBundle("~/bundles/JGalleryCss")
               .Include("~/Content/plugins/jquery-gallery/jgallery.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap")
                .Include("~/Content/js/popper.js",
                         "~/Content/js/bootstrap.js"));

            bundles.Add(new StyleBundle("~/bundles/datatablesCSS")
                .Include("~/Content/plugins/datatables/css/dataTables.bootstrap4.min.css"
                ));


            bundles.Add(new ScriptBundle("~/bundles/datatables")
                .Include("~/Content/plugins/datatables/js/jquery.dataTables.min.js",
                         "~/Content/plugins/datatables/js/dataTables.bootstrap4.min.js",
                         "~/Content/plugins/datatables/js/dataTables.buttons.min.js",
                         "~/Content/plugins/datatables/js/jszip.min.js",
                         "~/Content/plugins/datatables/js/pdfmake.min.js",
                         "~/Content/plugins/datatables/js/vfs_fonts.js",
                         "~/Content/plugins/datatables/js/buttons.html5.min.js",
                         "~/Content/plugins/datatables/js/buttons.print.min.js",
                         "~/Content/plugins/datatables/js/datetime-moment.js",
                         "~/Content/plugins/datatables/js/sorting-date-datatable.js",
                         "~/Content/plugins/datatables/js/datetime.js"
                         ));

            bundles.Add(new ScriptBundle("~/bundles/toastr")
                .Include("~/Content/plugins/toastr/toastr.js"));

            bundles.Add(new ScriptBundle("~/bundles/ckeditor")
                .Include("~/Content/plugins/ckeditor/ckeditor.js",
                         "~/Content/plugins/ckeditor/translations/pt-br.js"));

            bundles.Add(new ScriptBundle("~/bundles/moment")
              .Include("~/Content/plugins/moment/moment.js",
                "~/Content/plugins/moment/moment-with-locales.js"));

            bundles.Add(new StyleBundle("~/bundles/tagsCSS")
               .Include("~/Content/plugins/tags/tagsinput.css"));

            bundles.Add(new ScriptBundle("~/bundles/tags")
                .Include("~/Content/plugins/tags/tagsinput.js"));

            bundles.Add(new StyleBundle("~/bundles/select2CSS")
             .Include("~/Content/plugins/select2/css/select2.css"));

            bundles.Add(new ScriptBundle("~/bundles/select2")
                .Include("~/Content/plugins/select2/js/select2.min.js"));

            bundles.Add(new StyleBundle("~/bundles/bootstrap4ToggleCSS")
             .Include("~/Content/plugins/bootstrap4-toggle/bootstrap4-toggle.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap4Toggle")
                .Include("~/Content/plugins/bootstrap4-toggle/bootstrap4-toggle.min.js"));



            bundles.Add(new ScriptBundle("~/bundles/JGallery")
                .Include("~/Content/plugins/jquery-gallery/jgallery.js",
                         "~/Content/plugins/jquery-gallery/touchswipe.min.js"
                ));
        }
    }
}
