using System;
using Microsoft.Extensions.DependencyInjection;
using OpenTabletDriver.Plugin.Components;
using OpenTabletDriver.Tablet;
using OpenTabletDriver.Vendors.XP_Pen;
using Xunit;

namespace OpenTabletDriver.Tests
{
    public class ReportParserProviderTest
    {
        public static TheoryData<string, Type> ReportParserProvider_CanGet_ReportParsers_Data => new()
        {
            { typeof(TabletReportParser).FullName!, typeof(TabletReportParser) },
            { typeof(XP_PenReportParser).FullName!, typeof(XP_PenReportParser) }
        };

        [Theory]
        [MemberData(nameof(ReportParserProvider_CanGet_ReportParsers_Data))]
        public void ReportParserProvider_CanGet_ReportParsers(string reportParserName, Type expectedReportParserType)
        {
            var serviceCollection = new DriverServiceCollection();
            var reportParserProvider = serviceCollection.BuildServiceProvider()
                .GetRequiredService<IReportParserProvider>();

            var reportParserType = reportParserProvider.GetReportParser(reportParserName).GetType();

            Assert.Equal(expectedReportParserType, reportParserType);
        }
    }
}