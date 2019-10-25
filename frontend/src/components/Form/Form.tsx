import React from "react";

export interface Values {
    event_name: string;
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
            event_name: "",
            values: [],
            submitSuccess: false,
        }
    }

    private processFormSubmission = (e: React.FormEvent<HTMLFormElement>): void => {
        e.preventDefault();
        const formData = {
            event_name: this.state.event_name,
        }
        this.setState({ submitSuccess: true, values: [...this.state.values, formData]});
        console.log("Event Name: " + this.state.event_name);
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
                        <label htmlFor="event_name">Event Name: </label>
                        <input
                            type="text"
                            id="event_name"
                            onChange={(e) => this.handleInputChanges(e)}
                            name="event_name"
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