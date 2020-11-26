import React, { Component } from "react";
import DynamicForm from "./components/DynamicForm";
import "./App.css";
import Button from '@material-ui/core/Button';
import { makeStyles } from '@material-ui/core/styles';


class App extends Component {
  state = {
    data: [
      {
        id: 0,
        name: "doggie",
        status: ["available", "not available"],
        photoUrls: [
           "string"
            ]
      },
      // {
        
      //   "category": {
      //     "id": 0,
      //     "name": "string"
      //   },
      
      //   "photoUrls": [
      //     "string"
      //   ],
      //   "tags": [
      //     {
      //       "id": 0,
      //       "name": "string"
      //     }
      //   ],
      
      // }
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
          <td>{d.name}</td>
          <td>{d.category}</td>
          <td>{d.status && d.status.join(",")}</td>
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
          title="ADD PET"
          defaultValues={this.state.current}
          model={[
            { key: "name", label: "Name", props: { required: true } },
           
            {
              key: "status",
              label: "Status",
              type: "checkbox",
              options: [
                { key: "available", label: "Available", value: "available" },
                { key: "not available", label: "Not Available", value: "not available" }
              ]
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

export default App;