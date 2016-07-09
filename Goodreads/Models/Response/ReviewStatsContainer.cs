using System.Collections.Generic;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// Since ReviewStats are returned in JSON for some reason, this container
    /// class is simply used for deserialization through RestSharp. It's internal
    /// since we don't actually want to expose it through the client API.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Microsoft.Performance",
        "CA1812:AvoidUninstantiatedInternalClasses",
        Justification = "Used in deserialization.")]
    internal class ReviewStatsContainer
    {
        public List<ReviewStats> Books { get; set; }
    }
}
