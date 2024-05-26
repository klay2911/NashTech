const Pokemon = (props) => {
    return (
        <div style={{ fontSize: '3em' }}>
            <div>ID: {props?.data?.id}</div>
            <div>Name: {props?.data?.name}</div>
            <div>Weight: {props?.data?.weight}</div>
            <div>
                {props?.data?.sprites && (
                    <img src={props.data.sprites.front_default} alt={props.data.name} style={{ width: '200px', height: '200px' }} />
                )}
                {props?.data?.sprites && (
                    <img src={props.data.sprites.back_default} alt={props.data.name} style={{ width: '200px', height: '200px' }} />
                )}
            </div>
        </div>
    );
}

export default Pokemon;