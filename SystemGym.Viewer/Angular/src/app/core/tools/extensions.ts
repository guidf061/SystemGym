export { }

declare global {
  interface StringConstructor {
    isNullOrEmpty(value: string): boolean;
  }

  interface String {
    parseFloatLocale(locale: string): number;
  }

  interface Number {
    parseFloatLocale(locale: string): number;
    truncateToDecimals(decimals: number): number;
  }
}

 String.isNullOrEmpty = function (value: string) {
  return value === undefined || value === null || value.length === 0;
};

String.prototype.parseFloatLocale = function (locale = 'pt-BR') {
  let textNumber: string = this.toString();

  if (!String.isNullOrEmpty(textNumber)) {
    textNumber = textNumber.replace(/[^\d,-]/g, '').replace(/[,]/g, '.');
    if (textNumber !== '') {
      return parseFloat(textNumber);
    }
  }

  return null;
};

Number.prototype.parseFloatLocale = function (locale = 'pt-BR') {
  if (this instanceof Number) {
    return this;
  }

  return this.toString().parseFloatLocale(locale);
};

Number.prototype.truncateToDecimals = function (decimals: number) {
  let fixed: string = this.toFixed(15);
  return parseFloat(fixed.substring(0, fixed.indexOf(".") + decimals + 1));
};
