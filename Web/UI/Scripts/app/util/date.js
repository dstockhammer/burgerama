// <reference path="../../app.ts" />
var Burgerama;
(function (Burgerama) {
    (function (Util) {
        function convertDateStringsToDates(input) {
            var regexIso8601 = /^([\+-]?\d{4}(?!\d{2}\b))((-?)((0[1-9]|1[0-2])(\3([12]\d|0[1-9]|3[01]))?|W([0-4]\d|5[0-2])(-?[1-7])?|(00[1-9]|0[1-9]\d|[12]\d{2}|3([0-5]\d|6[1-6])))([T\s]((([01]\d|2[0-3])((:?)[0-5]\d)?|24\:?00)([\.,]\d+(?!:))?)?(\17[0-5]\d([\.,]\d+)?)?([zZ]|([\+-])([01]\d|2[0-3]):?([0-5]\d)?)?)?)?$/;

            // Ignore things that aren't objects
            if (typeof input !== "object")
                return input;

            for (var key in input) {
                if (!input.hasOwnProperty(key))
                    continue;

                var value = input[key];
                var match;

                // Check for string properties which look like dates
                if (typeof value === "string" && (match = value.match(regexIso8601))) {
                    var milliseconds = Date.parse(match[0]);
                    if (!isNaN(milliseconds)) {
                        input[key] = new Date(milliseconds);
                    }
                } else if (typeof value === "object") {
                    // Recurse into object
                    convertDateStringsToDates(value);
                }
            }
        }
        Util.convertDateStringsToDates = convertDateStringsToDates;
    })(Burgerama.Util || (Burgerama.Util = {}));
    var Util = Burgerama.Util;
})(Burgerama || (Burgerama = {}));
//# sourceMappingURL=date.js.map
