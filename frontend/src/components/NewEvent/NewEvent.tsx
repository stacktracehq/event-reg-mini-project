import React from "react";
import axios from 'axios';
import {
    EventSaveRequest, StartTime
} from "../../models/models"
import { RouteComponentProps } from "react-router-dom";
import styles from "./NewEvent.module.css";
import { Guid } from "guid-typescript";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {faPlusSquare} from "@fortawesome/free-regular-svg-icons"

export class NewEvent extends React.Component<RouteComponentProps, EventSaveRequest & StartTime> {
    constructor(props: RouteComponentProps) {
        super(props);
        this.state = {
            event: null,
            errors: false,
            errorMessage: " ",
            startHour: "09",
            startMinute: "00",
        }
    }

    private processFormSubmission = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        console.log(this.state)
        let request = {
            ...this.state.event!,
            id: Guid.raw()
        }
        await axios.post<Event>(`https://localhost:5001/v1/events`, request)
                .then(response => {
                    const { history } = this.props;
                    history.push('/');
                })
                .catch(error => {
                    this.setState({ errorMessage: error.response.data.Message });
                    this.setState({ errors: true });
                });
    }

    private updateEventName = (e: React.FormEvent<HTMLInputElement>) => {
        this.setState({
            ...this.state,
            event: {
                ...this.state.event!,
                title: { value: e.currentTarget.value }
            }
        })
    }

    private updateEventDescription = (e: React.FormEvent<HTMLTextAreaElement>) => {
        this.setState({
            ...this.state,
            event: {
                ...this.state.event!,
                description: { value: e.currentTarget.value }
            }
        })
    }

    private updateEventLocation = (e: React.FormEvent<HTMLInputElement>) => {
        this.setState({
            ...this.state,
            event: {
                ...this.state.event!,
                eventLocation: { value: e.currentTarget.value }
            }
        })
    }

    private updateEventStartDate = (e: React.FormEvent<HTMLInputElement>) => {
        this.setState({
            ...this.state,
            event: {
                ...this.state.event!,
                eventStartDate: { value: e.currentTarget.value }
            }
        })
    }

    private updateEventEndDate = (e: React.FormEvent<HTMLInputElement>) => {
        this.setState({
            ...this.state,
            event: {
                ...this.state.event!,
                eventEndDate: { value: e.currentTarget.value }
            }
        })
    }

    private updateRegistrationOpenDate = (e: React.FormEvent<HTMLInputElement>) => {
        this.setState({
            ...this.state,
            event: {
                ...this.state.event!,
                registrationOpenDate: { value: e.currentTarget.value }
            }
        })
    }

    private updateRegistrationCloseDate = (e: React.FormEvent<HTMLInputElement>) => {
        this.setState({
            ...this.state,
            event: {
                ...this.state.event!,
                registrationCloseDate: { value: e.currentTarget.value }
            }
        })
    }

    private updateEventStartHours = (e: React.FormEvent<HTMLSelectElement>) => {
        this.setState({
            startHour:  e.currentTarget.value
        })
    }

    private updateEventStartMinutes = (e: React.FormEvent<HTMLSelectElement>) => {
            this.setState({
                startMinute: e.currentTarget.value
            })
        }

    private updateEventStartMinutesBasedOnAmPm = (e: React.FormEvent<HTMLSelectElement>) => {
            let currentHour = (document.getElementById("startHours") as HTMLInputElement).value;
            if ( e.currentTarget.value === "pm" ) {
                this.setState({
                    startHour: `${parseInt(currentHour) + 12}`
                })
            } 
        }

    public render() {
        const {errors, errorMessage} = this.state;
        return (
            <div className={styles.main}>
                <h1 className="new-event-header">Create a New Event</h1>
                    {errors && errorMessage===undefined && (
                        <div className={styles.ohno} role="alert">
                            Oops, something went wrong
                            </div>
                    )}
                    {errors && (
                        <div className={styles.ohno} role="alert">
                            {errorMessage}
                        </div>
                  )}
                <form onSubmit={this.processFormSubmission} noValidate={true}>
                    <div className={styles.labelInputDiv}>
                        <label htmlFor="title" className={styles.label}>Event Name: </label>
                        <input
                            type="text"
                            id="title"
                            onChange={(e) => this.updateEventName(e)}
                            name="title"
                            className={styles.input}
                        />
                    </div>
                    <div className={styles.labelInputDiv}>
                        <label htmlFor="description" className={[ styles.label , styles.textareaLabel ].join(" ")}>Description: </label>
                        <textarea
                            id="description"
                            onChange={(e) => this.updateEventDescription(e)}
                            name="description"
                            className={styles.input}
                            rows={ 5 }
                        />
                    </div>
                    <div className={styles.labelInputDiv}>
                        <label htmlFor="eventLocation" className={styles.label}>Location:</label>
                        <input
                            type="text"
                            id="eventLocation"
                            onChange={(e) => this.updateEventLocation(e)}
                            name="eventLocation"
                            placeholder=""
                            className={styles.input}
                        />
                    </div>

                    &nbsp;
                    <div className={styles.labelInputDiv}>
                        <label htmlFor="eventStartDate" className={styles.label}>Start Date: </label>
                        <input
                            type="date"
                            id="eventStartDate"
                            onChange={(e) => this.updateEventStartDate(e)}
                            name="eventStartDate"
                            placeholder=""
                            className={styles.input}
                        />
                    </div>
                    <div className={styles.labelInputDiv}>
                        <label htmlFor="eventStartTime" className={styles.label}>Start Time: </label>
                        <select
                            id="startHours"
                            className={styles.dropdown}
                            onChange={(e) => this.updateEventStartHours(e)}
                            defaultValue="09"
                        >
                            <option value="01">1</option>
                            <option value="02">2</option>
                            <option value="03">3</option>
                            <option value="04">4</option>
                            <option value="05">5</option>
                            <option value="06">6</option>
                            <option value="07">7</option>
                            <option value="08">8</option>
                            <option value="09">9</option>
                            <option value="10">10</option>
                            <option value="11">11</option>
                            <option value="12">12</option>
                        </select>
                        &nbsp;
                        <select
                            id="minutes"
                            className={styles.dropdown}
                            onChange={(e) => this.updateEventStartMinutes(e)}
                        >
                            <option value="0">0</option>
                            <option value="15">15</option>
                            <option value="30">30</option>
                            <option value="45">45</option>
                        </select>
                        &nbsp;
                        <select
                            id="ampm"
                            className={styles.dropdown}
                            onChange={(e) => this.updateEventStartMinutesBasedOnAmPm(e)}
                        >
                            <option value="am">AM</option>
                            <option value="pm">PM</option>
                        </select>
                    </div>
                    &nbsp;

                <div className={styles.labelInputDiv}>
                    <label htmlFor="eventEndDate" className={styles.label}>End Date: </label>
                        <input
                            type="date"
                            id="eventEndDate"
                            onChange={(e) => this.updateEventEndDate(e)}
                            name="eventEndDate"
                            placeholder=""
                            className={styles.input}
                        />
                    </div>

                    <div className={styles.labelInputDiv}>
                        <label htmlFor="registrationOpenDate" className={styles.label}>Registration Open Date: </label>
                        <input
                            type="date"
                            id="registrationOpenDate"
                            onChange={(e) => this.updateRegistrationOpenDate(e)}
                            name="registrationOpenDate"
                            placeholder=""
                            className={styles.input}
                        />
                    </div>
                    <div className={styles.labelInputDiv}>
                        <label htmlFor="registrationCloseDate" className={styles.label}>Registration Close Date: </label>
                        <input
                            type="date"
                            id="registrationCloseDate"
                            onChange={(e) => this.updateRegistrationCloseDate(e)}
                            name="registrationCloseDate"
                            placeholder=""
                            className={styles.input}
                        />
                    </div>

                    <div>
                        <button type="submit" className={styles.button}>
                            <FontAwesomeIcon icon={faPlusSquare} fixedWidth size="sm"/>{" "}
                            Create an Event
                        </button>

                    </div>
                </form>
            </div>
        )
    }
}

export default NewEvent;