import { EventTime, AmPm } from "../models/models";

export function fromEventTimeToADate(eventTime: EventTime): Date {
    const date = new Date(eventTime.date);
    date.setHours(fromEventTimeAdjustHourToPm(eventTime) + 10);
    date.setMinutes(eventTime.minute);
    return date;
}

export function fromDateToEventTime(date: Date): EventTime {
    const eventTime: EventTime = {
        date: new Date(date.toDateString()),
        hour: adjustHourFrom24HrTime(date.getHours()),
        minute: date.getMinutes(),
        amPm: adjustAmPm(date.getHours())
    }

    return eventTime;
}

export function fromEventTimeAdjustHourToPm(eventTime: EventTime): number {
    let hour = eventTime.hour;
    if (hour === 12 && eventTime.amPm === "am") {
        hour = 0
    };

    if (eventTime.amPm === "pm" && eventTime.hour !== 12) {
        hour = eventTime.hour + 12
    };
    return hour;
}

export function adjustHourFrom24HrTime(hour: number): number {
    if (hour > 12) {
        return hour - 12;
    }
    return hour
}

export function adjustAmPm(hour: number): AmPm {
    if (hour >= 12) {
        return "pm";
    }
    return "am";
}

export function formatHour(hour: number): string {
    if (hour === 0) {
        return `0${hour}`
    }
    return `${hour}`
}

export function formatMinutes(minutes: number): string {
    if (minutes < 10) {
        return `0${minutes}`
    }
    return `${minutes}`
}

export function formatDateTimeForPrettyPrint(dateTime: Date | string): string {
    if (typeof dateTime === 'string') {
        dateTime = new Date(dateTime);
    }
    let year = dateTime.getFullYear();
    let month = dateTime.getMonth();
    let day = dateTime.getDate();
    let hour = dateTime.getHours();
    let minutes = dateTime.getMinutes();

    const monthNames = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];

    let time = `${formatHour(adjustHourFrom24HrTime(hour))}:${formatMinutes(minutes)}${adjustAmPm(hour)}`
    return `${day} ${monthNames[month]} ${year}, ${time}`
}