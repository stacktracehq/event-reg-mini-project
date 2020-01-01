import "./dateTimeHelpers"
import { EventTime } from "../models/models"
import {
    fromEventTimeToADate,
    fromEventTimeAdjustHourToPm,
    fromDateToEventTime,
    adjustHourFrom24HrTime,
    adjustAmPm,
    formatDateTimeForPrettyPrint
} from "./dateTimeHelpers"

describe('fromEventTimeToADate', () => {
    it('31 December 2019, 10:30pm', () => {
        const eventTime: EventTime = {
            date: new Date("31 December 2019"),
            hour: 10,
            minute: 30,
            amPm: "pm"
        }

        const date = fromEventTimeToADate(eventTime);
        const expected = new Date("31 December 2019 22:30")
        expect(date).toStrictEqual(expected);
    })
})

describe('fromDateToEventTime', () => {
    it('converts date to an EventTime', () => {
        const date = new Date("31 December 2019 22:30");
        const expectedEventTime = {
            date: new Date("31 December 2019"),
            hour: 10,
            minute: 30,
            amPm: "pm"
        }

        const eventTime = fromDateToEventTime(date);
        expect(eventTime).toStrictEqual(expectedEventTime);
    })
})

describe('fromEventTimeAdjustHourToPm', () => {
    it("adjusts time to 24hr time if it is past 12pm", () => {
        const eventTime: EventTime = {
            date: new Date("24 April 2010"),
            hour: 4,
            minute: 0,
            amPm: "pm"
        }

        const hour = fromEventTimeAdjustHourToPm(eventTime);
        const expected = 16;
        expect(hour).toBe(expected);
    });

    it("stays as is if it is in the morning", () => {
        const eventTime: EventTime = {
            date: new Date("24 April 2010"),
            hour: 4,
            minute: 0,
            amPm: "am"
        }

        const hour = fromEventTimeAdjustHourToPm(eventTime);
        const expected = 4;
        expect(hour).toBe(expected);
    });

    it("stays as 12 if the time is 12pm", () => {
        const eventTime: EventTime = {
            date: new Date("24 April 2010"),
            hour: 12,
            minute: 0,
            amPm: "pm"
        }

        const hour = fromEventTimeAdjustHourToPm(eventTime);
        const expected = 12;
        expect(hour).toBe(expected);
    })
})

describe("adjustHourFrom24HrTime", () => {
    it('adjusts hour greater than 12 to 12 hour time', () => {
        const hour = 14;
        const adjustedHour = adjustHourFrom24HrTime(hour);

        expect(adjustedHour).toBe(2);
    });

    it('does not change the time if hour is less than or equal to 12', () => {
        const hour = 2;
        const adjustedHour = adjustHourFrom24HrTime(hour);

        expect(adjustedHour).toBe(2);
    });
})

describe("adjustAmPm", () => {
    it('returns pm if hour greater than 12', () => {
        const hour = 14;
        const amOrPm = adjustAmPm(hour);

        expect(amOrPm).toBe("pm");
    });

    it('returns pm if hour is equal to 12', () => {
        const hour = 12;
        const amOrPm = adjustAmPm(hour);

        expect(amOrPm).toBe("pm");
    });

    it('returns am if hour is less than 12', () => {
        const hour = 3;
        const amOrPm = adjustAmPm(hour);

        expect(amOrPm).toBe("am");
    });
})

describe("formatDateTimeForPrettyPrint", () => {
    it('returns date time in proper format', () => {
        const inputDate = new Date("24 April 2020 15:30");
        const dateToPrettify = formatDateTimeForPrettyPrint(inputDate);
        const expected = "24 April 2020, 3:30pm";

        expect(dateToPrettify).toBe(expected);
    });

    it('has a leading 0 if it is midnight', () => {
        const inputDate = new Date("24 April 2020 00:45");
        const dateToPrettify = formatDateTimeForPrettyPrint(inputDate);
        const expected = "24 April 2020, 00:45am";

        expect(dateToPrettify).toBe(expected);
    });

    it('has a leading 0 if the minutes are less than 10', () => {
        const inputDate = new Date("24 April 2020 03:00");
        const dateToPrettify = formatDateTimeForPrettyPrint(inputDate);
        const expected = "24 April 2020, 3:00am";

        expect(dateToPrettify).toBe(expected);
    });
})