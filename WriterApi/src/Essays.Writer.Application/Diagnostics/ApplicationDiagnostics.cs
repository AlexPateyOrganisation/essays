using System.Diagnostics.Metrics;

namespace Essays.Writer.Application.Diagnostics;

public static class ApplicationDiagnostics
{
    private const string ServiceName = "essays-writer-api";
    public static readonly Meter Meter = new(ServiceName);

    public static readonly Counter<long> EssaysCreatedCounter
        = Meter.CreateCounter<long>("essays-created");
}