import React from "react";

export interface Values {
    event_name: string;
}

export class Form extends React.Component {
    constructor(props: any) {
        super(props);
        this.state = {
            event_name: ""
        }
    }

    public render() {
        return (
            <div>
                <h2>Create a New Event</h2>
                <form>
                    <div>
                        <label htmlFor="event_name">Event Name: </label>
                        <input type="text" id="event_name" name="event_name" placeholder="Enter a title for your event" />
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