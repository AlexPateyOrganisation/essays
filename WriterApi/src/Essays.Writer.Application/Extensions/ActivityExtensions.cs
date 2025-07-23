using System.Diagnostics;
using Essays.Core.Models.Models;

namespace Essays.Writer.Application.Extensions;

public static class ActivityExtensions
{
    public static void EnrichWithEssay(this Activity? activity, Essay essay)
    {
        activity?.AddTag("essay-id", essay.Id);
        activity?.AddTag("essay-title", essay.Title);
        activity?.AddTag("essay-author", essay.Author);
    }

    public static void EnrichWithEssayId(this Activity? activity, Guid essayId)
    {
        activity?.AddTag("essay-id", essayId);
    }
}