﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using ExcelDna.Integration;
using ExcelDna.Logging;

namespace ACQ.Excel
{
    public static class Info
    {
        [ExcelFunction(Description = "Returns version of ACQ add-in", Category = AddInInfo.Category)]
        public static string acq_version()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
        [ExcelFunction(Description = "Returns version of Excel", Category = AddInInfo.Category)]
        public static object acq_excel_version()
        {
            return ExcelDnaUtil.ExcelVersion;
        }

        [ExcelFunction(Description = "Returns the version of the Excel-DNA library", Category = AddInInfo.Category)]
        public static object acq_exceldna_version()
        {
            string filename = acq_xllpath();
            FileVersionInfo info = FileVersionInfo.GetVersionInfo(filename);
            return info.ProductVersion;
        }

        [ExcelFunction(Description = "Returns the version of the .NET", Category = AddInInfo.Category)]
        public static object acq_dotnet_version()
        {
            return System.Environment.Version.ToString();
        }


        [ExcelFunction(Description = "Returns the location of the ACQ add-in", Category = AddInInfo.Category)]
        public static string acq_xllpath()
        {
            return ExcelDnaUtil.XllPath;
        }
        [ExcelCommand(MenuText = "Show Log Window", MenuName = AddInInfo.MenuName)]
        public static void ShowLogWindow()
        {
            LogDisplay.Show();
        }


        [ExcelCommand(MenuText = "Show Introspection Window", MenuName = AddInInfo.MenuName)]
        public static void ShowIntrospectionWindow()
        {
            ExcelReference cell = (ExcelReference)XlCall.Excel(XlCall.xlfActiveCell);

            object value = cell.GetValue();

            if (value != null)
            {
                string handle = value.ToString();

                object acq_object;

                if (ACQ.Excel.Handles.GlobalCache.TryGetObject(handle, out acq_object))
                {
                    ACQ.Excel.Introspection.IntrospectionDlg dlg = new ACQ.Excel.Introspection.IntrospectionDlg(acq_object);

                    dlg.Show();
                }
            }
        }
    }
}
