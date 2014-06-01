module Burgerama.Util {
    export function getApiUrl(service: string): string {
        return config.url.api.replace('{service}', service);
    }

    export function isApiUrl(url: string): boolean {
        var cleanApiUrl = config.url.api.replace('{service}', '~~any~~');
        var regexApiUrl = '^' + escapeRegex(cleanApiUrl);
        regexApiUrl = regexApiUrl.replace('~~any~~', '.*');

        return url.match(regexApiUrl) != null;
    }

    function escapeRegex(input: string): string {
        return input.replace(/[\-\[\]\/\{\}\(\)\*\+\?\.\\\^\$\|]/g, "\\$&");
    }
}