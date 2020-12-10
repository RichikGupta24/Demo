import React from "react";
import Table from "@material-ui/core/Table";
import TableBody from "@material-ui/core/TableBody";
import TableCell from "@material-ui/core/TableCell";
import TableContainer from "@material-ui/core/TableContainer";
import TableHead from "@material-ui/core/TableHead";
import TableRow from "@material-ui/core/TableRow";
import Paper from "@material-ui/core/Paper";
const PetRow = ({ pet }) => (
  <TableRow key={pet.id}>
    <TableCell component="th" scope="row">
      {pet.id}
    </TableCell>
    <TableCell>{pet.category.name}</TableCell>
    <TableCell> {pet.name}</TableCell>
    <TableCell>
      {" "}
      <ul>
        {pet.photoUrls.map((sub) => (
          <li>{sub}</li>
        ))}
      </ul>
    </TableCell>
    <TableCell>
      <ul>
        {pet.tags.map((sub1) => (
          <li>{sub1.name}</li>
        ))}
      </ul>
    </TableCell>
    <TableCell> {pet.status}</TableCell>
  </TableRow>
);
export default PetRow;
