import Table from "@material-ui/core/Table";
import TableBody from "@material-ui/core/TableBody";
import TableCell from "@material-ui/core/TableCell";
import TableContainer from "@material-ui/core/TableContainer";
import TableHead from "@material-ui/core/TableHead";
import TableRow from "@material-ui/core/TableRow";
import Paper from "@material-ui/core/Paper";
import React, { useEffect, useState, Fragment } from "react";
import PetRow from "./Pet";
const PetList = () => {
  const pets = [
    {
      id: 0,
      category: {
        id: 0,
        name: "home_animal",
      },
      name: "doggie",
      photoUrls: ["https://xyz?qwsQ1.com", "https://abc?qwsQ1.com"],
      tags: [
        {
          id: 0,
          name: "Abc",
        },
        {
          id: 1,
          name: "dbc",
        },
      ],
      status: "available",
    },
  ];

  return (
    <>
      <h1>List of Pets</h1>
      <div style={{ padding: "20px" }}>
        <TableContainer component={Paper}>
          <Table aria-label="simple table">
            <TableHead>
              <TableRow>
                <TableCell>id</TableCell>
                <TableCell>category</TableCell>
                <TableCell>name</TableCell>
                <TableCell>photoUrls</TableCell>
                <TableCell>tags</TableCell>
                <TableCell>status</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {pets.map((pet, index) => (
                <PetRow pet={pet} key={index} />
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      </div>
    </>
  );
};
export default PetList;
