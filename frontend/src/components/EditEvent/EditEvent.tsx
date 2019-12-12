import React from "react";
import axios from "axios";
import { RouteComponentProps } from "react-router";
import { EventSaveRequest, EventId, Event } from "../../models/models";
import styles from "./EditEvent.module.css"
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEdit } from "@fortawesome/free-regular-svg-icons";
import { faSpinner } from "@fortawesome/free-solid-svg-icons";

export class EditEvent extends React.Component<RouteComponentProps<EventId>, EventSaveRequest> {
        constructor(props: RouteComponentProps<EventId>) {
        super(props);
        this.state = {
            event: null,
            errors: false,
            errorMessage: "",
        }
    }

    componentDidMount() {
        axios.get<Event>(`https://localhost:5001/v1/events/${this.props.match.params.id}`)
            .then( response  =>{
                this.setState({
                    event: response.data
                })
            })
            .catch(error => {
                console.log(error)
            })
    }

    private processFormSubmission = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        await axios.put(`https://localhost:5001/v1/events/${this.props.match.params.id}`, this.state.event)
                .then(() => {
                    const { history } = this.props;
                    history.push(`/event/${this.props.match.params.id}`);
                })
                .catch(error => {
                    console.log(error.response);
                    this.setState({ errors: true });
                });
    }

    private updateEventName = (e: React.FormEvent<HTMLInputElement>) => {
        this.setState({
            event: {
                ...this.state.event!,
                title: { value: e.currentTarget.value }
            }
        })
    }

    private updateEventDescription = (e: React.FormEvent<HTMLTextAreaElement>) => {
        this.setState({
            event: {
                ...this.state.event!,
            description: { value: e.currentTarget.value }
            }
        })
    }

    private updateEventLocation = (e: React.FormEvent<HTMLInputElement>) => {
        this.setState({
            event: {
                ...this.state.event!,
            eventLocation: { value: e.currentTarget.value }
            }
        })
    }

    private updateEventStartDate = (e: React.FormEvent<HTMLInputElement>) => {
        this.setState({
            event: {
                ...this.state.event!,
            eventStartDate: { value: e.currentTarget.value }
            }
        })
    }

    private updateEventEndDate = (e: React.FormEvent<HTMLInputElement>) => {
        this.setState({
            event: {
                ...this.state.event!,
            eventEndDate: { value: e.currentTarget.value }
            }
        })
    }
  private updateRegistrationOpenDate = (e: React.FormEvent<HTMLInputElement>) => {
        this.setState({
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
        const { errors} = this.state;
        if (this.state.event !== null) {
            return (

                <div className={styles.main}>
                    <h1 className="new-event-header">
                        Edit Event:  {this.state.event!.title.value}
                    </h1>
                    {errors && (
                        <div className="alert alert-info alert-error" role="alert">
                            Oops, something went wrong
                            </div>
                    )}
                    <form onSubmit={this.processFormSubmission} noValidate={true} >
                        <div className={styles.labelInputDiv}>
                            <label htmlFor="title" className={styles.label}>Event Name: </label>
                            <input
                                type="text"
                                id="title"
                                onChange={(e) => this.updateEventName(e)}
                                name="title"
                                value={ this.state.event!.title.value }
                                className={styles.input}
                            />
                        </div>
                        <div className={styles.labelInputDiv}>
                            <label htmlFor="description" className={[ styles.label, styles.textareaLabel ].join(" ")}>Description: </label>
                            <textarea
                                id="description"
                                onChange={(e) => this.updateEventDescription(e)}
                                name="description"
                                value={ this.state.event!.description.value }
                                className={styles.input}
                                rows={5}
                            />
                        </div>
                        <div className={styles.labelInputDiv}>
                            <label htmlFor="eventLocation" className={styles.label}>Location:</label>
                            <input
                                type="text"
                                id="eventLocation"
                                onChange={(e) => this.updateEventLocation(e)}
                                name="eventLocation"
                                value={this.state.event!.eventLocation.value}
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
                                value={this.state.event!.eventStartDate.value.toString().substr(0,10)}
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
                                value={this.state.event!.eventEndDate.value.toString().substr(0,10)}
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
                                value={this.state.event!.registrationOpenDate.value.toString().substr(0,10)}
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
                                value={this.state.event!.registrationCloseDate.value.toString().substr(0,10)}
                                className={styles.input}
                            />
                        </div>

                        <div>
                            <button type="submit" className={styles.button}>
                                <FontAwesomeIcon icon={faEdit} fixedWidth size="sm"  />
                                {" "}
                                Update Event
                            </button>

                        </div>
                    </form>
                </div>


            )
        } else {
            return (
                <div className={styles.main}>
                    <div className={styles.loadingContainer}>
                        <p className={styles.loading}>
                            <FontAwesomeIcon icon={faSpinner} spin fixedWidth />
                            Loading...
                        </p>
                    </div>
                </div>
            )
        }
    }

}

export default EditEvent;