import { Injectable } from '@angular/core';
import { NgbDateAdapter, NgbDateParserFormatter, NgbDatepickerI18n, NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import {DatepickerTranslations} from "./models/DatapickerTranslation";

@Injectable()
export class CustomDatepickerI18n extends NgbDatepickerI18n {
  getWeekdayShortName(weekday: number): string {
    return DatepickerTranslations.weekdays[weekday - 1];
  }

  getMonthShortName(month: number): string {
    return DatepickerTranslations.months[month - 1];
  }

  getMonthFullName(month: number): string {
    return this.getMonthShortName(month);
  }

  getDayAriaLabel(date: NgbDateStruct): string {
    return `${date.day}.${date.month}.${date.year}`;
  }

  getWeekdayLabel(weekday: number, width?: any): string {
    return "";
  }
}

/**
 * This Service handles how the date is represented in scripts i.e. ngModel.
 */
@Injectable()
export class CustomAdapter extends NgbDateAdapter<string> {
  readonly DELIMITER = '.';

  fromModel(value: string | null): NgbDateStruct | null {
    if (value) {
      const date = value.split(this.DELIMITER);
      return {
        day: parseInt(date[0], 10),
        month: parseInt(date[1], 10),
        year: parseInt(date[2], 10)
      };
    }
    return null;
  }

  toModel(date: NgbDateStruct | null): string | null {
    return date ? ('0' + date.day).substr(-2) + this.DELIMITER + ('0' + date.month).substr(-2) + this.DELIMITER + date.year : '';
  }
}

/**
 * This Service handles how the date is rendered and parsed from keyboard i.e. in the bound input field.
 */
@Injectable()
export class CustomDateParserFormatter extends NgbDateParserFormatter {
  readonly DELIMITER = '.';

  parse(value: string): NgbDateStruct | null {
    if (value) {
      const date = value.split(this.DELIMITER);
      return {
        day: parseInt(date[0], 10),
        month: parseInt(date[1], 10),
        year: parseInt(date[2], 10)
      };
    }
    return null;
  }

  format(date: NgbDateStruct | null): string {
    return date ? ('0' + date.day).substr(-2) + this.DELIMITER + ('0' + date.month).substr(-2) + this.DELIMITER + date.year : '';
  }
}
