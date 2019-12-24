import React from "react";
import axios from "axios";
import { RouteComponentProps } from "react-router";
import { EventSaveRequest, EventId, Event, StartTime } from "../../models/models";
import styles from "./EditEvent.module.css"
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEdit } from "@fortawesome/free-regular-svg-icons";
import { faSpinner } from "@fortawesome/free-solid-svg-icons";

export class EditEvent extends React.Component<RouteComponentProps<EventId>, EventSaveRequest & StartTime> {
        constructor(props: RouteComponentProps<EventId>) {
        super(props);
        this.state = {
            event: null,
            errors: false,
            errorMessage: "",
            startHour: "00",
            startMinute: "00",
            endHour: "00",
            endMinute: "00"
        }
    }

    componentDidMount() {
        axios.get<Event>(`https://localhost:5001/v1/events/${this.props.match.params.id}`)
            .then( response  =>{
                this.setState({
                    event: response.data
                })
            console.log(this.state.event!.eventStartDate.value.toString().substr(12,  1));
            })
            .catch(error => {
                this.setState({ errorMessage: error.response.data.Message });
                this.setState({ errors: true});
            })
    }


    private adjustStartTimeToPm = () => {
        let ampm = (document.getElementById("ampm") as HTMLInputElement).value;
        let currentHour = (document.getElementById("startHours") as HTMLInputElement).value;

        if (currentHour === "12" && ampm === "am") {
            this.setState({
                startHour: "00"
            })
        }

        if (ampm === "pm" && this.state.startHour !== "12") {
            this.setState({
                startHour: `${parseInt(currentHour) + 12}`
            })
        }
    }

    private adjustEndTimeToPm = () => {
        let ampm = (document.getElementById("endampm") as HTMLInputElement).value;
        let currentHour = (document.getElementById("endHours") as HTMLInputElement).value;

        if (currentHour === "12" && ampm === "am") {
            this.setState({
                endHour: "00"
            })
        }

        if (ampm === "pm" && this.state.endHour !== "12") {
            this.setState({
                endHour: `${parseInt(currentHour) + 12}`
            })
        }
    }

    private makeStartDateTimeHaveADateAndATime = () => {
        // create a datetime so we can add some hours to it - wont let me with a EventStartDate type
        let settingStartDateTime = new Date(this.state.event!.eventStartDate.value + "Z");

        // zero the minutes in the datetime, just incase a form has an error, isn't submitted and the user changes the time.
        settingStartDateTime.setHours(0);
        settingStartDateTime.setMinutes(0);

        // add on the hours from the form - the +10 is a terrible terrible workaround because Chrome adds on my timezone and yes I know this is terrible but it works...
        settingStartDateTime.setHours((settingStartDateTime.getHours() + parseInt(this.state.startHour)) + 10);
        settingStartDateTime.setMinutes((settingStartDateTime.getMinutes() + parseInt(this.state.startMinute)));
        this.setState({
            ...this.state,
            event: {
                ...this.state.event!,
                eventStartDate: { value: settingStartDateTime }
            }
        })
    }

    // This is the same function as above, I couldn't seem to make it work with variables :(
    private makeEndDateTimeHaveADateAndATime = () => {
        let settingEndDateTime = new Date(this.state.event!.eventEndDate.value + "Z");

        settingEndDateTime.setHours(0);
        settingEndDateTime.setMinutes(0);

        settingEndDateTime.setHours((settingEndDateTime.getHours() + parseInt(this.state.endHour)) + 10);
        settingEndDateTime.setMinutes((settingEndDateTime.getMinutes() + parseInt(this.state.endMinute)));
        this.setState({
            ...this.state,
            event: {
                ...this.state.event!,
                eventEndDate: { value: settingEndDateTime }
            }
        })
    }

    private processFormSubmission = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        await this.adjustStartTimeToPm();
        await this.adjustEndTimeToPm();

        await this.makeStartDateTimeHaveADateAndATime();
        await this.makeEndDateTimeHaveADateAndATime();

        await axios.put(`https://localhost:5001/v1/events/${this.props.match.params.id}`, this.state.event)
                .then(() => {
                    const { history } = this.props;
                    history.push(`/event/${this.props.match.params.id}`);
                })
                .catch(error => {
                    this.setState({ errorMessage: error.response.data.Message });
                    this.setState({ errors: true});
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

    private updateEventEndHours = (e: React.FormEvent<HTMLSelectElement>) => {
        this.setState({
            endHour:  e.currentTarget.value
        })
    }

    private updateEventEndMinutes = (e: React.FormEvent<HTMLSelectElement>) => {
        this.setState({
            endMinute: e.currentTarget.value
        })
    }

    private isTheTimeAmOrPm = (eventTime: number) => {
        if (eventTime >= 12) {
            return "pm"
        } else {
            return "am"
        }
    }

    public render() {
        const { errors, errorMessage } = this.state;
        if (this.state.event !== null) {
            return (

                <div className={styles.main}>
                    <h1 className="new-event-header">
                        Edit Event:  {this.state.event!.title.value}
                    </h1>
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
                        <label htmlFor="eventStartTime" className={styles.label}>Start Time: </label>
                        <select
                            id="startHours"
                            className={styles.dropdown}
                            onChange={(e) => this.updateEventStartHours(e)}
                            value={this.state.event!.eventStartDate.value.toString().substr(11, 2)}
                        >
                            <option value="1">1</option>
                            <option value="2">2</option>
                            <option value="3">3</option>
                            <option value="4">4</option>
                            <option value="5">5</option>
                            <option value="6">6</option>
                            <option value="7">7</option>
                            <option value="8">8</option>
                            <option value="9">9</option>
                            <option value="10">10</option>
                            <option value="11">11</option>
                            <option value="12">12</option>
                        </select>
                        &nbsp;
                        <select
                            id="minutes"
                            className={styles.dropdown}
                            onChange={(e) => this.updateEventStartMinutes(e)}
                            value={this.state.event!.eventStartDate.value.toString().substr(14, 2)}
                        >
                            <option value="0">00</option>
                            <option value="15">15</option>
                            <option value="30">30</option>
                            <option value="45">45</option>
                        </select>
                        &nbsp;
                        <select
                            id="ampm"
                            className={styles.dropdown}
                            defaultValue={this.isTheTimeAmOrPm(parseInt(this.state.event!.eventStartDate.value.toString().substr(11,2)))}
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
                                value={this.state.event!.eventEndDate.value.toString().substr(0,10)}
                                className={styles.input}
                            />
                        </div>

                              <div className={styles.labelInputDiv}>
                        <label htmlFor="eventEndTime" className={styles.label}>End Time: </label>
                        <select
                            id="endHours"
                            className={styles.dropdown}
                            onChange={(e) => this.updateEventEndHours(e)}
                            value={this.state.event!.eventEndDate.value.toString().substr(11, 2)}
                        >
                            <option value="1">1</option>
                            <option value="2">2</option>
                            <option value="3">3</option>
                            <option value="4">4</option>
                            <option value="5">5</option>
                            <option value="6">6</option>
                            <option value="7">7</option>
                            <option value="8">8</option>
                            <option value="9">9</option>
                            <option value="10">10</option>
                            <option value="11">11</option>
                            <option value="12">12</option>
                        </select>
                        &nbsp;
                        <select
                            id="eventEndMinutes"
                            className={styles.dropdown}
                            onChange={(e) => this.updateEventEndMinutes(e)}
                            value={this.state.event!.eventEndDate.value.toString().substr(14, 2)}
                        >
                            <option value="0">00</option>
                            <option value="15">15</option>
                            <option value="30">30</option>
                            <option value="45">45</option>
                        </select>
                        &nbsp;
                        <select
                            id="endampm"
                            className={styles.dropdown}
                            defaultValue={this.isTheTimeAmOrPm(parseInt(this.state.event!.eventEndDate.value.toString().substr(11,2)))}
                        >
                            <option value="am">AM</option>
                            <option value="pm">PM</option>
                        </select>
                    </div>
                    &nbsp;

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