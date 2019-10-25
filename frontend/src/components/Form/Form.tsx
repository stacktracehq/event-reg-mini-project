import React from "react";
import axios from 'axios';

export interface EventTitle {
    value: string
}

export interface Values {
    title: EventTitle;
}

export interface FormState {
    [key: string]: any;
    values: Values[];
    submitSuccess: boolean;
}

export class Form extends React.Component<{}, FormState> {
    constructor(props: any) {
        super(props);
        this.state = {
            title: {value: ""},
            values: [],
            submitSuccess: false,
        }
    }

    private processFormSubmission = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        const formData = {
            title: {value: this.state.title},
        }
        this.setState({ submitSuccess: true, values: [...this.state.values, formData]});
        await axios.post(`https://localhost:5001/v1/events`, formData)
                .then(response => console.log(response))
                .catch(error => console.log(error));
        console.log("Event Name: " + this.state.title);
        console.log(formData);
    }

    private handleInputChanges = (e: React.FormEvent<HTMLInputElement>) => {
        e.preventDefault();
        this.setState({
            [e.currentTarget.name]: e.currentTarget.value,
        })
    }

    public render() {
        const { submitSuccess} = this.state;
        return (
            <div>
                <h2>Create a New Event</h2>
                {!submitSuccess && (
                      <div className="alert alert-info" role="alert">
                          Fill the form below to create a new event
                  </div>
                  )}
                  {submitSuccess && (
                      <div className="alert alert-info" role="alert">
                          The form was successfully submitted!
                          </div>
                  )}
                <form onSubmit={this.processFormSubmission} noValidate={true}>
                    <div>
                        <label htmlFor="title">Event Name: </label>
                        <input
                            type="text"
                            id="title"
                            onChange={(e) => this.handleInputChanges(e)}
                            name="title"
                            placeholder="Enter a title for your event"
                        />
                    </div>
                    <div>
                        <button type="submit">
                            Create an Event
                        </button>
                    </div>
                </form>
            </div>
        )
    }
}

export default Form;