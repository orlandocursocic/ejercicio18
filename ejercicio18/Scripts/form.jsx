    
class Formulario extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      nombre: '',
      apellidos: '',
      edad: 0
    };

    this.handleInputChange = this.handleInputChange.bind(this);
    this.componentDidMount = this.componentDidMount.bind(this);
    this.handleLimpiar = this.handleLimpiar.bind(this);
  }

  handleInputChange(event) {
    const target = event.target;
    const value = target.value;
    const name = target.name;

    this.setState({
      [name]: value
        
    });
  }
 
  handleLimpiar(){
    this.setState(initialState);
    //this.setState({
    //      nombre: nombreInicial,
    //      apellidos: apellidosInicial,
    //      edad: edadInicial
    //  });
  }

    
  componentDidMount() {
      //Hace la llamada get y luego la magia con lambdas y la de dios
      $.get("api/Personas/1", (data) => this.realizaActualizacion(data));
  }

  realizaActualizacion(data) {
      let nombre = data.Nombre;
      this.setState({
          nombre: data.Nombre,
          apellidos: data.Apellidos,
          edad: data.Edad
      });
  }

  

    

  render() { 
    return (
      <form action="/" method="POST" id="personalDataForm">
        <label>
          Nombre: 
          <input
                 id="nombre"
            name="nombre"
            type="text"
            maxLength="20"
            value={this.state.nombre}
            onChange={this.handleInputChange} />
        </label>
        <br />
        <label>
          Apellidos: 
          <input
                 id="apellidos"
            name="apellidos"
            type="text"
            maxLength="20"
            value={this.state.apellidos}
            onChange={this.handleInputChange} />
        </label>
        <br />
        <label>
          Edad: 
          <input
                 id="edad"
            name="edad"
            type="number"
            value={this.state.edad}
            min={0}
            max={120}
            onChange={this.handleInputChange} />
        </label>

          <br />
        <label>          
          <input
                 id="enviar"
            name="enviar"
            type="submit"
            value="Actualizar"
            onChange={this.handleSubmitChange} />
        </label>
           <label>
          <input name="limpiar"
                 type="button"
                 value="Limpiar"
                 onClick={this.handleLimpiar} />
           </label>


      </form>
    );
  }
}

ReactDOM.render(
  <Formulario />,
  document.getElementById('root')
);
