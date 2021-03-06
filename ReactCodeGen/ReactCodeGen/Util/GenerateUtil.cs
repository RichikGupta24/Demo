﻿using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using ReactCodeGen.Entities;

namespace ReactCodeGen.Util
{
    public class GenerateUtil
    {
        private static Dictionary<int, int[]> gridRow = new Dictionary<int, int[]>()
        {
            {1, new int[] {12} },
            {2, new int[] {6,6} },
            {3, new int[] {4,4,4} },
            {4, new int[] {3,3,3,3} }
        };

        public static string GenerateReactCode(Request request)
        {
            List<ReactCode> codeGens = new List<ReactCode>();

            foreach (var design in request.Design)
            {
                var rows = gridRow[design.Count];
                for (int i = 0; i < design.Count; i++)
                {
                    if (design[i].Type != "array")
                    {
                        switch (design[i].Control)
                        {
                            case "1":
                                {
                                    codeGens.Add(DrawTextBox(design[i], "text", rows[i]));
                                    break;
                                }
                            case "2":
                                {
                                    codeGens.Add(DrawTextBox(design[i], "number", rows[i]));
                                    break;
                                }
                            case "3":
                                {
                                    codeGens.Add(DrawTextBox(design[i], "email", rows[i]));
                                    break;
                                }
                            case "4":
                                {
                                    codeGens.Add(DrawTextBox(design[i], "date", rows[i]));
                                    break;
                                }
                            case "5":
                                {
                                    //TODO File
                                    break;
                                }
                            case "6":
                                {
                                    codeGens.Add(DrawSelect(design[i], rows[i]));
                                    break;
                                }
                        }
                    }
                    else
                    {
                        if (design[i].Items.Count == 0)
                        {
                            codeGens.Add(DrawArray(design[i], rows[i]));
                        }
                    }
                }
            }

            codeGens.Add(DrawSubmitModel(request, codeGens));

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("import logo from './logo.svg';");
            sb.AppendLine("import React, {useCallback, useState} from 'react'");
            sb.AppendLine("import Grid from '@material-ui/core/Grid';");
            sb.AppendLine("import { makeStyles } from '@material-ui/core/styles';");
            sb.AppendLine("import TextField from '@material-ui/core/TextField';");
            sb.AppendLine("import Select from '@material-ui/core/Select';");
            sb.AppendLine("import MenuItem from '@material-ui/core/MenuItem';");
            sb.AppendLine("import InputLabel from '@material-ui/core/InputLabel';");
            sb.AppendLine("import FormHelperText from '@material-ui/core/FormHelperText';");
            sb.AppendLine("import Button from '@material-ui/core/Button';");
            sb.AppendLine("import './App.css';");

            sb.AppendLine("const useStyles = makeStyles((theme) => ({ root: { flexGrow: 1, padding: 10 }, rightMargin: { marginRight: 120 }, rightFloat: { float: 'right', top: -40 } }));");

            sb.AppendLine("function App() {");
            sb.AppendLine("const classes = useStyles();");

            foreach (var gen in codeGens)
            {
                foreach (var state in gen.States)
                {
                    sb.AppendLine(state.Value);
                }
            }

            foreach (var gen in codeGens)
            {
                foreach(var func in gen.Function)
                {
                    sb.AppendLine(func);
                }
            }

            sb.AppendLine("return (");
            sb.AppendLine("<form className={classes.root} noValidate autoComplete='off' onSubmit={onSubmit}>");
            sb.AppendLine("<h1>" + request.Header + "</h1>");
            sb.AppendLine("<Grid container spacing={3}>");

            foreach(var gen in codeGens)
            {
                sb.AppendLine(gen.Html);
            }

            sb.AppendLine("");

            sb.AppendLine("</Grid>");
            sb.AppendLine("</form>");
            sb.AppendLine(");");
            sb.AppendLine("}");

            sb.AppendLine("export default App;");
            File.WriteAllText(@"C:\Users\462676\Desktop\codeGen\my-app\src\App.js", sb.ToString());

            return sb.ToString();
        }

        private static ReactCode DrawTextBox(RequestDesign design, string type, int grid)
        {
            ReactCode codeGen = new ReactCode();

            StringBuilder sbHtml = new StringBuilder();
            sbHtml.AppendLine("<Grid item xs={" + grid.ToString() + "}>");
            sbHtml.AppendLineFormat("<TextField label='{0}' helperText='{1}' type='{2}' {3} {4} fullWidth />",
                design.Level, design.Description, type, design.Required ? "required" : "",
                "onInput={ e=>Set_" + design.ObjectName + "(e.target.value)}");
            sbHtml.AppendLine("</Grid>");
            codeGen.Html = sbHtml.ToString();

            codeGen.States.Add(design.ObjectName, string.Format("const [{0}, {1}] = useState('{2}')", design.ObjectName, "Set_" + design.ObjectName, design.Value));

            codeGen.Imports.Add("import TextField from '@material-ui/core/TextField';");

            return codeGen;
        }

        private static ReactCode DrawArray(RequestDesign design, int grid)
        {
            ReactCode codeGen = new ReactCode();

            StringBuilder sbHtml = new StringBuilder();
            sbHtml.AppendLine("<Grid item xs={" + grid.ToString() + "}>");
            sbHtml.AppendLine("<Grid item xs={12}>");
            sbHtml.AppendLine("<Grid item xs={12} className={classes.rightMargin}>");
            sbHtml.AppendLineFormat("<TextField label='{0}' helperText='{1}' type='text' {2} {3} fullWidth />",
                design.Level, design.Description, design.Required ? "required" : "",
                "onInput={ e=>Set_" + design.ObjectName + "(e.target.value, 0)}");
            sbHtml.AppendLine("</Grid>");
            sbHtml.AppendLineFormat("<Button variant='contained' className={0} onClick={1}>Add</Button>", "{classes.rightFloat}", "{Add_" + design.ObjectName + "}");
            sbHtml.AppendLine("</Grid>");

            sbHtml.AppendLine("{"+ design.ObjectName + ".map((value, index) => {");
            sbHtml.AppendLine("if (index > 0) {");
            sbHtml.AppendLine("return (");
            sbHtml.AppendLine("<Grid key={`tag-${index}`} item xs={12}>");
            sbHtml.AppendLine("<Grid item xs={12} className={classes.rightMargin}>");
            sbHtml.AppendLineFormat("<TextField label='{0}' type='text' {1} fullWidth />", design.Level, "onInput={ e=>Set_" + design.ObjectName + "(e.target.value, index)}");
            sbHtml.AppendLine("</Grid>");
            sbHtml.AppendLineFormat("<Button variant='contained' className={0} onClick={1}>Remove</Button>", "{classes.rightFloat}", "{e=> Remove_" + design.ObjectName + "(index)}");
            sbHtml.AppendLine("</Grid>");
            sbHtml.AppendLine(")");
            sbHtml.AppendLine("}");
            sbHtml.AppendLine("})}");

            sbHtml.AppendLine("</Grid>");
            codeGen.Html = sbHtml.ToString();

            codeGen.States.Add(design.ObjectName, string.Format("const [{0}, {1}] = useState(['{2}'])", design.ObjectName, "Set_Array_" + design.ObjectName, design.Value));

            StringBuilder sbFunc = new StringBuilder();
            //Set Array value function
            sbFunc.AppendLineFormat("function Set_{0}(val, index)", design.ObjectName);
            sbFunc.AppendLine("{");
            sbFunc.AppendLineFormat("{0}[index]=val;", design.ObjectName);
            sbFunc.AppendLineFormat("Set_Array_{0}([...{0}]);", design.ObjectName);
            sbFunc.AppendLine("}");

            // Add Array
            sbFunc.AppendLineFormat("function Add_{0}()", design.ObjectName);
            sbFunc.AppendLine("{");
            sbFunc.AppendLineFormat("Set_Array_{0}([...{0}, '']);", design.ObjectName);
            sbFunc.AppendLine("}");

            //Remove Array
            sbFunc.AppendLineFormat("function Remove_{0}(index)", design.ObjectName);
            sbFunc.AppendLine("{");
            sbFunc.AppendLineFormat("{0}.splice(index,1);", design.ObjectName);
            sbFunc.AppendLineFormat("Set_Array_{0}([...{0}]);", design.ObjectName);
            sbFunc.AppendLine("}");
            codeGen.Function.Add(sbFunc.ToString());

            codeGen.Imports.Add("import TextField from '@material-ui/core/TextField';");

            return codeGen;
        }

        private static ReactCode DrawSelect(RequestDesign design, int grid)
        {
            ReactCode codeGen = new ReactCode();

            StringBuilder sbHtml = new StringBuilder();
            sbHtml.AppendLine("<Grid item xs={" + grid.ToString() + "}>");
            sbHtml.AppendLineFormat("<InputLabel id='{0}'>{1}</InputLabel>", design.ObjectName, design.Level);
            sbHtml.AppendLineFormat("<Select labelId = '{0}' {1} fullWidth {2}>", design.ObjectName, design.Required ? "required" : "",
                "onChange={ e=>Set_" + design.ObjectName + "(e.target.value)}");
            foreach (var value in design.Values)
            {
                sbHtml.AppendLineFormat("<MenuItem value={0}>{1}</MenuItem>", "{'" + value + "'}", value);
            }
            sbHtml.AppendLine("</Select>");
            sbHtml.AppendLineFormat("<FormHelperText>{0}</FormHelperText>", design.Description);
            sbHtml.AppendLine("</Grid>");
            codeGen.Html = sbHtml.ToString();

            codeGen.States.Add(design.ObjectName, string.Format("const [{0}, {1}] = useState('{2}')", design.ObjectName, "Set_" + design.ObjectName, design.Value));

            codeGen.Imports.Add("import Select from '@material-ui/core/Select';");
            codeGen.Imports.Add("import MenuItem from '@material-ui/core/MenuItem';");
            codeGen.Imports.Add("import InputLabel from '@material-ui/core/InputLabel';");
            codeGen.Imports.Add("import FormHelperText from '@material-ui/core/FormHelperText';");

            return codeGen;
        }

        private static ReactCode DrawSubmitModel(Request request, List<ReactCode> gens)
        {
            ReactCode codeGen = new ReactCode();

            Dictionary<string, string> states = new Dictionary<string, string>();
            string postModel = GeneratePostModel(request.Operation, ref states);

            string url = "'" + request.Server + request.Operation.Name + "'";
            foreach (var param in request.Operation.Params)
            {
                url = url.Replace("{" + param.Name + "}", "'+query_" + param.Name + "+'");
                states.Add("query_" + param.Name, param.Type);
            }
            url = url.Replace("+''", "");

            var usedState = new List<string>();
            foreach (var gen in gens)
            {
                foreach (var state in gen.States)
                {
                    usedState.Add(state.Key);
                }
            }

            foreach (var state in states)
            {
                if (!usedState.Contains(state.Key))
                {
                    codeGen.States.Add(state.Key, string.Format("const [{0}, {1}] = useState({2})", state.Key, "Set_" + state.Key,
                        state.Value == "integer" ? "0" : (state.Value == "array" ? "[]" : "''")));
                }
            }

            StringBuilder sbFunc = new StringBuilder();
            sbFunc.AppendLine("const onSubmit = useCallback((event) => {");
            sbFunc.AppendLine("event.preventDefault();");

            sbFunc.AppendLine("const requestOptions = {");
            sbFunc.AppendLineFormat("method: '{0}',", request.Operation.Verb.ToUpper());
            sbFunc.AppendLine("headers: { 'Content-Type': 'application/json' },");
            if (request.Operation.Verb == "Post" || request.Operation.Verb == "Put")
            {
                sbFunc.AppendLineFormat("body: JSON.stringify({0})", postModel);
            }
            sbFunc.AppendLine("};");

            sbFunc.AppendLineFormat("fetch({0}, requestOptions)", url);
            sbFunc.AppendLine(".then(response => response.json())");
            sbFunc.AppendLine(".then(data => console.log(data))");
            sbFunc.AppendLine(".catch (err => console.log(err));");

            sbFunc.AppendLine("})");

            codeGen.Function.Add(sbFunc.ToString());

            codeGen.Html = "<Grid item xs={12}><Button variant='contained' color='primary' type='submit'>Submit</Button></Grid>";

            codeGen.Imports.Add("import Button from '@material-ui/core/Button';");

            return codeGen;
        }

        private static string GeneratePostModel(OpenApiOperation operation, ref Dictionary<string, string> states)
        {
            if (operation.Verb == "Post" || operation.Verb == "Put")
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("{");
                foreach (var param in operation.BodyParams)
                {
                    if (param.Property.Count == 0 || param.Type == "array")
                    {
                        sb.AppendFormat("{0}: body_{0},", param.Name);
                        states.Add("body_" + param.Name, param.Type);
                    }
                    else
                    {
                        sb.AppendFormat("{0}:", param.Name);
                        GeneratePostModelRecursive(ref sb, "body_" + param.Name, param.Property, ref states);
                    }
                }
                sb.Append("}");

                return sb.ToString();
            }
            return null;
        }

        private static void GeneratePostModelRecursive(ref StringBuilder sb, string key, List<OpenApiOperationParam> param, ref Dictionary<string, string> states)
        {
            sb.Append("{");
            foreach (var p in param)
            {
                if (p.Property.Count == 0 || p.Type == "array")
                {
                    sb.AppendFormat("{0}: {1}_{0},", p.Name, key);
                    states.Add(key + "_" + p.Name, p.Type);
                }
                else
                {
                    sb.AppendFormat("{0}:", p.Name);
                    GeneratePostModelRecursive(ref sb, key + "_" + p.Name, p.Property, ref states);
                }
            }
            sb.Append("},");
        }
    }
}