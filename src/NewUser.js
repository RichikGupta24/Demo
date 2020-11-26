import React, { Component } from "react";
import DynamicForm from "./components/DynamicForm";
import "./App.css";

class NewUser extends Component {
  state = {
    data: [
        {
            id: 0,
            username: "string",
            firstName: "string",
            lastName: "string",
            email: "string",
            password: "string",
            phone: "string",
            userStatus: 0
          }
      
    ],
    current: {}
  };

  onSubmit = model => {
    let data = [];
    if (model.id) {
      data = this.state.data.filter(d => {
        return d.id != model.id;
      });
    } else {
      model.id = +new Date();
      data = this.state.data.slice();
    }

    this.setState({
      data: [model, ...data],
      current: {} // todo
    });
  };

  onEdit = id => {
    let record = this.state.data.find(d => {
      return d.id == id;
    });
    //alert(JSON.stringify(record));
    this.setState({
      current: record
    });
  };

  onNewClick = e => {
    this.setState({
      current: {}
    });
  };

  render() {
    let data = this.state.data.map(d => {
      return (
        <tr key={d.id}>
          <td>{d.username}</td>
          <td>{d.firstName}</td>
          <td>{d.lastName}</td>
          <td>{d.email}</td>
          <td>{d.password}</td>
          <td>{d.phone}</td>
          <td>{d.userStatus}</td>
          <td>
            <button
              onClick={() => {
                this.onEdit(d.id);
              }}
            >
              edit
            </button>
          </td>
        </tr>
      );
    });

    return (

      
      <div className="App">
        <div className="form-actions">
          <button onClick={this.onNewClick} type="submit">
            NEW
          </button>
        </div>
        <DynamicForm
          key={this.state.current.id}
          className="form"
          title="ADD USER"
          defaultValues={this.state.current}
          model={[
            { key: "username", label: "Username", props: { required: true } },
            { key: "firstName", label: "First Name" },
            { key: "lastName", label: "Last Name" },
            { key: "email", label: "Email" },
            { key: "password", label: "Password" },
            { key: "Phone", label: "phone"},
            {
                key: "userStatus",
                label: "User Status",
                type: "number",
                props: { min: 0, max: 1 }
              }
           
          ]}
          onSubmit={model => {
            this.onSubmit(model);
          }}
        />

        <table border="1">
          <tbody>{data}</tbody>
        </table>
      </div>
    );
  }
}

export default NewUser;