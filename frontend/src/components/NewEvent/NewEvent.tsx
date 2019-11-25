import React from "react";
import axios from 'axios';
import {
    EventSaveRequest
} from "../../models/models"
import { RouteComponentProps } from "react-router-dom";
import styles from "./NewEvent.module.css";
import { Guid } from "guid-typescript";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {faPlusSquare} from "@fortawesome/free-regular-svg-icons"

export class NewEvent extends React.Component<{} & RouteComponentProps, EventSaveRequest> {
    constructor(props: RouteComponentProps) {
        super(props);
        this.state = {
            event: null,
            errors: false,
        }
    }

    private processFormSubmission = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        let request = {
            ...this.state.event!,
            id: Guid.raw()
        }
        // this.setState((state, props) => ({
        //     event: {
        //         ...state.event!,
        //         id: Guid.raw()

        //     }
        // }));
        console.log(this.state)
        await axios.post<Event>(`https://localhost:5001/v1/events`, request)
                .then(response => {
                    const { history } = this.props;
                    history.push('/');
                })
                .catch(error => {
                    console.log(error);
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

    public render() {
        const {errors} = this.state;
        return (
            <div className={styles.main}>
                <h1 className="new-event-header">Create a New Event</h1>
                    {errors && (
                      <div className={styles.ohno} role="alert">
                          Oops, something went wrong
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