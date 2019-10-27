const dollarSign = '$';
const emptyString = '';
const comma = ',';
const period = '.';
const minus = '-';
const minusRegExp = /-/;
const nonDigitsRegExp = /\D+/g;
const number = 'number';
const digitRegExp = /\d/;
const anyRegExp = /[^\n]+/;
const caretTrap = '[]';

export class CustomMask {

  static numberMask({
    prefix = '',
    includeThousandsSeparator = true,
    thousandsSeparatorSymbol = period,
    allowDecimal = false,
    decimalSymbol = comma,
    decimalLimit = 2,
    allowNegative = false,
    allowLeadingZeroes = true,
    integerLimit = 16
  } = {}) {
    const prefixLength = prefix && prefix.length || 0
    const thousandsSeparatorSymbolLength = thousandsSeparatorSymbol && thousandsSeparatorSymbol.length || 0

    return function (rawValue = emptyString) {

      const rawValueLength = rawValue.length;

      if (rawValue === emptyString || (rawValue[0] === prefix[0] && rawValueLength === 1)) {
        return prefix.split(emptyString).concat([digitRegExp + '']);
      }

      const indexOfLastDecimal = rawValue.lastIndexOf(decimalSymbol)
      const hasDecimal = indexOfLastDecimal !== -1;
      const isNegative = (rawValue[0] === minus) && allowNegative;

      let integer: string;
      let fraction;
      let mask = [];

      integer = rawValue.replace(nonDigitsRegExp, emptyString);

      if (allowDecimal) {
        if (integer.length > 1) {
          if (integer.length > decimalLimit) {
            fraction = CustomMask.convertToMask(integer.substring(integer.length - decimalLimit));
            integer = integer.substring(0, integer.length - decimalLimit);
          }
          else if (integer.length === 4) {
            fraction = CustomMask.convertToMask(integer.substring(integer.length - 3));
            integer = integer.substring(0, integer.length - 3);
          }
          else if (integer.length === 3) {
            fraction = CustomMask.convertToMask(integer.substring(integer.length - 2));
            integer = integer.substring(0, integer.length - 2);
          }
          else if (integer.length === 2) {
            fraction = CustomMask.convertToMask(integer.substring(integer.length - 1));
            integer = integer.substring(0, integer.length - 1);
          }
        }
      }
      else {
        if (rawValue.slice(0, prefixLength) === prefix) {
          integer = rawValue.slice(prefixLength);
        } else {
          integer = rawValue;
        }
      }

      if (integerLimit && typeof integerLimit === number) {
        const thousandsSeparatorRegex = thousandsSeparatorSymbol === '.' ? '[.]' : `${thousandsSeparatorSymbol}`;
        const numberOfThousandSeparators = (integer.match(new RegExp(thousandsSeparatorRegex, 'g')) || []).length;

        integer = integer.slice(0, integerLimit + (numberOfThousandSeparators * thousandsSeparatorSymbolLength));
      }

      integer = integer.replace(nonDigitsRegExp, emptyString);

      if (!allowLeadingZeroes) {
        integer = String(Number(integer));
      }

      integer = (includeThousandsSeparator) ? CustomMask.addThousandsSeparator(integer, thousandsSeparatorSymbol) : integer;

      mask = CustomMask.convertToMask(integer);

      if (allowDecimal) {
        if (fraction) {
          mask.push(decimalSymbol);
          mask = mask.concat(fraction);
        }
      }

      if (prefixLength > 0) {
        mask = prefix.split(emptyString).concat(mask);
      }

      if (isNegative) {
        // If user is entering a negative number, add a mask placeholder spot to attract the caret to it.
        if (mask.length === prefixLength) {
          mask.push(digitRegExp);
        }

        mask = [minusRegExp].concat(mask);
      }

      return mask;
    }
  }

  static createAutoCorrectedNumber(decimal: number = 2) {
    return function (conformedValue: string) {
      console.log(conformedValue);

      let value: string = conformedValue;

      if (conformedValue.length >= 1 && conformedValue.length < decimal + 2) {
        value = CustomMask.pad(conformedValue, decimal + 1);
      }

      return value;
    }
  }

  static dateMask(): any {
    return function (rawValue = emptyString) {
      let mask = [digitRegExp, digitRegExp, '/', digitRegExp, digitRegExp, '/', digitRegExp, digitRegExp, digitRegExp, digitRegExp];

      if (rawValue === emptyString) {
        return [digitRegExp];
      }

      let integer: string = CustomMask.Left(rawValue.replace(nonDigitsRegExp, emptyString), 12);
      let maskLength = integer.length;

      if (maskLength > 8) {
        maskLength += 3;
      }
      else if (maskLength > 4) {
        maskLength += 2;
      }
      else if (maskLength > 2) {
        maskLength += 1;
      }

      return mask.slice(0, maskLength);
    }
  }

  static dateTimeMask(): any {
    return function (rawValue = emptyString) {
      let mask = [digitRegExp, digitRegExp, '/', digitRegExp, digitRegExp, '/', digitRegExp, digitRegExp, digitRegExp, digitRegExp, ' ', digitRegExp, digitRegExp, ':', digitRegExp, digitRegExp];

      if (rawValue === emptyString) {
        return [digitRegExp];
      }

      let integer: string = CustomMask.Left(rawValue.replace(nonDigitsRegExp, emptyString), 12);
      let maskLength = integer.length;

      if (maskLength > 10) {
        maskLength += 4;
      }
      else if (maskLength > 8) {
        maskLength += 3;
      }
      else if (maskLength > 4) {
        maskLength += 2;
      }
      else if (maskLength > 2) {
        maskLength += 1;
      }

      return mask.slice(0, maskLength);
    }
  }

  static cpfMask(): any {
    return function (rawValue = emptyString) {
      let mask = [digitRegExp, digitRegExp, digitRegExp, '.', digitRegExp, digitRegExp, digitRegExp, '.', digitRegExp, digitRegExp, digitRegExp, '-', digitRegExp, digitRegExp];

      if (rawValue === emptyString) {
        return [digitRegExp];
      }

      let integer: string = CustomMask.Left(rawValue.replace(nonDigitsRegExp, emptyString), 11);
      let maskLength = integer.length;

      if (maskLength > 9) {
        maskLength += 3;
      }
      else if (maskLength > 6) {
        maskLength += 2;
      }
      else if (maskLength > 3) {
        maskLength += 1;
      }

      return mask.slice(0, maskLength);
    }
  }

  static cnpjMask(): any {
    return function (rawValue = emptyString) {
      let mask = [digitRegExp, digitRegExp, '.', digitRegExp, digitRegExp, digitRegExp, '.', digitRegExp, digitRegExp, digitRegExp, '/', digitRegExp, digitRegExp, digitRegExp, digitRegExp, '-', digitRegExp, digitRegExp];

      if (rawValue === emptyString) {
        return [digitRegExp];
      }

      let integer: string = CustomMask.Left(rawValue.replace(nonDigitsRegExp, emptyString), 14);
      let maskLength = integer.length;

      if (maskLength > 12) {
        maskLength += 4;
      }
      else if (maskLength > 8) {
        maskLength += 3;
      }
      else if (maskLength > 5) {
        maskLength += 2;
      }
      else if (maskLength > 2) {
        maskLength += 1;
      }

      return mask.slice(0, maskLength);
    }
  }

  static cepMask(): any {
    return function (rawValue = emptyString) {
      let mask = [digitRegExp, digitRegExp, digitRegExp, digitRegExp, digitRegExp, '-', digitRegExp, digitRegExp, digitRegExp];

      if (rawValue === emptyString) {
        return [digitRegExp];
      }

      let integer: string = CustomMask.Left(rawValue.replace(nonDigitsRegExp, emptyString), 8);
      let maskLength = integer.length;

      if (maskLength > 5) {
        maskLength += 1;
      }

      return mask.slice(0, maskLength);
    }
  }

  static phoneMask(): any {
    return function (rawValue = emptyString) {
      let mask = ['(', digitRegExp, digitRegExp, ')', ' ', digitRegExp, digitRegExp, digitRegExp, digitRegExp, '-', digitRegExp, digitRegExp, digitRegExp, digitRegExp];
      let mask9Dig = ['(', digitRegExp, digitRegExp, ')', ' ', digitRegExp, digitRegExp, digitRegExp, digitRegExp, digitRegExp, '-', digitRegExp, digitRegExp, digitRegExp, digitRegExp];

      if (rawValue === emptyString) {
        return [digitRegExp];
      }

      let integer: string = CustomMask.Left(rawValue.replace(nonDigitsRegExp, emptyString), 11);
      let maskLength = integer.length;

      if (maskLength > 6) {
        maskLength += 4;
      }
      else if (maskLength > 2) {
        maskLength += 3;
      }
      else if (maskLength > 1) {
        maskLength += 2;
      }
      else if (maskLength > 0) {
        maskLength += 1;
      }

      return (integer.length > 10 ? mask9Dig.slice(0, maskLength) : mask.slice(0, maskLength));
    }
  }

  static phoneInternationalMask(): any {
    return function (rawValue = emptyString) {
      let mask = ['+', digitRegExp, digitRegExp, ' ', digitRegExp, digitRegExp, ' ', digitRegExp, digitRegExp, digitRegExp, digitRegExp, '-', digitRegExp, digitRegExp, digitRegExp, digitRegExp];
      let mask9Dig = ['+', digitRegExp, digitRegExp, ' ', digitRegExp, digitRegExp, ' ', digitRegExp, digitRegExp, digitRegExp, digitRegExp, digitRegExp, '-', digitRegExp, digitRegExp, digitRegExp, digitRegExp];

      if (rawValue === emptyString) {
        return [digitRegExp];
      }

      let integer: string = CustomMask.Left(rawValue.replace(nonDigitsRegExp, emptyString), 13);
      let maskLength = integer.length;

      if (maskLength > 8) {
        maskLength += 4;
      }
      else if (maskLength > 3) {
        maskLength += 3;
      }
      else if (maskLength > 1) {
        maskLength += 2;
      }
      else if (maskLength > 0) {
        maskLength += 1;
      }

      return (integer.length > 11 ? mask9Dig.slice(0, maskLength) : mask.slice(0, maskLength));
    }
  }

  static infinityMask(): any {
    return function (rawValue = emptyString) {
      let mask = [];

      if (rawValue === '') {
        return [anyRegExp];
      }

      return rawValue
        .split(emptyString)
        .map((char) => anyRegExp);
    }
  }

  static convertToMask(strNumber) {
    return strNumber
      .split(emptyString)
      .map((char) => digitRegExp.test(char) ? digitRegExp : char);
  }

  static addThousandsSeparator(n, thousandsSeparatorSymbol) {
    return n.replace(/\B(?=(\d{3})+(?!\d))/g, thousandsSeparatorSymbol);
  }

  static Left(str, n) {
    if (n <= 0) {
      return "";
    }
    else if (n > String(str).length) {
      return str;
    }
    else {
      return String(str).substring(0, n);
    }
  }

  static pad(num: string, size: number): string {
    var s = num + "";
    while (s.length < size) s = "0" + s;
    return s;
  }
}
