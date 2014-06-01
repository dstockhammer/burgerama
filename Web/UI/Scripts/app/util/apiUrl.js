var Burgerama;
(function (Burgerama) {
    (function (Util) {
        function getApiUrl(service) {
            return config.url.api.replace('{service}', service);
        }
        Util.getApiUrl = getApiUrl;

        function isApiUrl(url) {
            var cleanApiUrl = config.url.api.replace('{service}', '~~any~~');
            var regexApiUrl = '^' + escapeRegex(cleanApiUrl);
            regexApiUrl = regexApiUrl.replace('~~any~~', '.*');

            return url.match(regexApiUrl) != null;
        }
        Util.isApiUrl = isApiUrl;

        function escapeRegex(input) {
            return input.replace(/[\-\[\]\/\{\}\(\)\*\+\?\.\\\^\$\|]/g, "\\$&");
        }
    })(Burgerama.Util || (Burgerama.Util = {}));
    var Util = Burgerama.Util;
})(Burgerama || (Burgerama = {}));
//# sourceMappingURL=apiUrl.js.map
