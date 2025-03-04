﻿using Infrastructure.Scraper;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;

namespace Infrastructure.Common.Services;

public class GoogleSearchEngineScrapeStrategy(IOptions<ScraperSettings> options, Lazy<IWebDriver> webDriver)
    : SearchEngineScrapeStrategy(options, webDriver)
{
    public override string Name => "Google";

    protected override string AnchorSelector => "a";
    protected override string ResultsSelector => "div.tF2Cxc";

    protected override string Url(string query)
        => string.Format("https://www.google.com/search?q={0}&num={1}", Uri.EscapeDataString(query), _maxResults);
}