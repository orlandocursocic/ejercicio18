class Formulario extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            previous: { nombre: '', apellidos: '', edad: 0 },
            actual: { nombre: '', apellidos: '', edad: 0 }
        }

        this.handleInputChange = this.handleInputChange.bind(this);
        this.componentDidMount = this.componentDidMount.bind(this);
        this.previousState = this.previousState.bind(this);
        this.cleanButton = this.cleanButton.bind(this);
    }

    handleInputChange(event) {
        const target = event.target;
        const value = target.value;
        const name = target.name;

        /*
        this.setState({
            [name] : value
        });
        */
        
        this.setState({
                actual: { [event.target.name]: event.target.value }
            });
        
    }


    componentDidMount() {
        //Hace la llamada get y luego la magia con lambdas y la de dios
        $.get("api/Personas/1", (data) => this.realizaActualizacion(data));
    }

    realizaActualizacion(data) {
        this.setState({
            actual: { nombre: data.Nombre, apellidos: data.Apellidos, edad: data.Edad },
            previous: { nombre: data.Nombre, apellidos: data.Apellidos, edad: data.Edad }
        });
    }

    previousState() {
        let prevNombre = this.state.previous.nombre;
        let prevApellidos = previous.apellidos;
        let prevEdad = previous.edad;

        this.setState({
            actual: {
                nombre: prevNombre,
                apellidos: prevApellidos,
                edad: prevEdad
            }
        });
    }

    cleanButton() {
        this.setState({
            actual: {
                nombre: '',
                apellidos: '',
                edad: 0
            },
        });
    }


    render() {
        return (
      <form action="/" method="POST" id="personalDataForm">
        <label>
            Nombre:
          <input id="nombre"
                 name="nombre"
                 type="text"
                 maxLength="20"
                 value={this.state.actual.nombre}
                 onChange={this.handleInputChange} />
        </label>
        <br />
        <label>
            Apellidos:
          <input id="apellidos"
                 name="apellidos"
                 type="text"
                 maxLength="20"
                 value={this.state.actual.apellidos}
                 onChange={this.handleInputChange} />
        </label>
        <br />
        <label>
            Edad:
          <input id="edad"
                 name="edad"
                 type="number"
                 value={this.state.actual.edad}
                 min={0}
                 max={120}
                 onChange={this.handleInputChange} />
        </label>

          <br />
        <label>
          <input id="previo"
                 name="previo"
                 type="button"
                 value="Previo"
                 onChange={this.previousState} />
        </label>
           <label>
          <input id="limpiar"
                 name="limpiar"
                 type="button"
                 value="Limpiar"
                 onClick={this.cleanButton} />
           </label>
      </form>
    );
    }
}

ReactDOM.render(
  <Formulario />,
  document.getElementById('root')
);
