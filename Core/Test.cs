using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Seagull.BarTender.Print;

namespace Core {
    public static class Test {
        public static void TestPrint(string value) {
            using (var engine = new Engine(true)) {
                //var printers = new Printers();

                var format = engine.Documents
                    .Open(
                        @"H:\codes\Whisky\Utils\test01.btw",
                        "TSC TTP-345");

                format.PrintSetup.IdenticalCopiesOfLabel = 1;
                format.PrintSetup.NumberOfSerializedLabels = 1;

                format.SubStrings["Name"].Value = value;

                format.Print("Test", 3000);
            }
        }
    }
}
