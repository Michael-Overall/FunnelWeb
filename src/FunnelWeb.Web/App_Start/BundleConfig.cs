﻿using System.Web.Optimization;
using FunnelWeb.Web.Application.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace FunnelWeb.Web.App_Start
{
    public static class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            /* SCRIPTS */

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/disqus-count").Include("~/Scripts/disqus-count.js"));
            bundles.Add(new ScriptBundle("~/bundles/jsdate").Include("~/Scripts/jsdate.js"));
            
            // Get the latest version from here: https://code.google.com/p/pagedown/source/browse/
            //bundles.Add(new ScriptBundle("~/bundles/pagedown").Include(
            //            "~/Scripts/Markdown.Converter.js",
            //            "~/Scripts/Markdown.Editor.js",
            //            "~/Scripts/Markdown.Sanitizer.js"));

            // Get the latest version from here: https://github.com/showdownjs/showdown
            bundles.Add(new ScriptBundle("~/bundles/showdown").Include(
                        "~/Scripts/showdown-0.4.0-20150301/compressed/Showdown.min.js",
                        "~/Scripts/showdown-0.4.0-20150301/compressed/extensions/github.min.js",
                        "~/Scripts/showdown-0.4.0-20150301/compressed/extensions/prettify.min.js",
                        "~/Scripts/showdown-0.4.0-20150301/compressed/extensions/table.min.js",
                        "~/Scripts/showdown-0.4.0-20150301/compressed/extensions/twitter.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/taggy").Include("~/Scripts/taggy.js"));

            // Get the latest version from here: https://code.google.com/p/google-code-prettify/source/browse/#svn%2Fbranches
            bundles.Add(new Bundle("~/bundles/prettify").Include("~/Scripts/prettify-20130304/prettify.js", "~/Scripts/prettify-20130304/lang-*"));
            
            bundles.Add(new ScriptBundle("~/bundles/site").Include("~/Scripts/site.js"));



            /* STYLES */
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                "~/Content/bootstrap/glyphicons.css",
                "~/Content/bootstrap/grid.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/baseCss").Include("~/Content/themes/base/Base.css"));
            bundles.Add(new StyleBundle("~/Content/themes/base/adminCss").Include("~/Content/themes/base/Base.css", "~/Content/themes/base/Admin.css"));

            /* THEME STYLES */
            FunnelWeb.Web.Application.Themes.ThemeProvider tp = new Application.Themes.ThemeProvider();
            string[] themes = tp.GetThemes();

            List<string> cssFiles = new List<string>();
            foreach (string theme in themes)
            {
                cssFiles.Clear();
                
                string themePath = "~/Themes/" + theme + "/";
                DirectoryInfo themeDir = new DirectoryInfo(HttpContext.Current.Server.MapPath(themePath));

                if (File.Exists(themeDir.FullName + "\\Content\\Styles\\Theme.css"))
                {
                    /* FunnelWeb theme */
                    cssFiles.Add(themePath + "\\Content\\Styles\\Theme.css");
                }
                else if (File.Exists(themeDir.FullName + "\\bootstrap.css"))
                {
                    /* Bootstrap theme */
                    cssFiles.Add(themePath + "bootstrap.css");
                }
                else if (File.Exists(themeDir.FullName + "\\style.css"))
                {
                    /* WordPress theme */
                    if (Directory.Exists(themeDir.FullName + "\\css"))
                    {
                        foreach (string cssFile in Directory.GetFiles(themeDir.FullName + "\\css"))
                        {
                            cssFiles.Add(themePath + "css/" + Path.GetFileName(cssFile));
                        }
                    }

                    cssFiles.Add(themePath + "style.css");
                    //cssFiles.Add(themePath + "editor-style.css");
                }

                if (cssFiles.Count > 0)
                {
                    cssFiles.Add(themePath + "custom.css");
                    bundles.Add(new StyleBundle("~/Themes/"+theme).Include(cssFiles.ToArray()));
                }
            }

            ViewBundleRegistrar.RegisterViewBundles(bundles);
        }

    }
}