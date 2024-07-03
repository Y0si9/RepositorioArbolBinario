// Configuraciones iniciales
const canvas = document.getElementById('arbolCanvas');
const ctx = canvas.getContext('2d');

canvas.width = window.innerWidth * 0.8;
canvas.height = window.innerHeight * 0.8;

const radius = 20; // Radio de los nodos
const verticalGap = 50; // Distancia vertical entre niveles del árbol
const horizontalGap = 30; // Distancia horizontal entre nodos

// Función para obtener el árbol desde el backend
async function obtenerArbol(tipo) {
    let url = '';
    switch (tipo) {
        case 'preorden':
            url = 'http://localhost:53245/api/arbol/preorden';
            break;
        case 'orden':
            url = 'http://localhost:53245/api/arbol/orden';
            break;
        case 'posorden':
            url = 'http://localhost:53245/api/arbol/posorden';
            break;
        case 'porniveles':
            url = 'http://localhost:53245/api/arbol/porniveles';
            break;
    }

    const response = await fetch(url);
    const data = await response.json();
    return JSON.parse(data); // Asegurarse de que la respuesta sea un objeto JSON válido
}

// Función para insertar un nodo en el árbol
async function insertarNodo() {
    const nodoValor = document.getElementById('nodoValor').value;
    if (!nodoValor) {
        alert('Por favor ingrese un valor para el nodo.');
        return;
    }

    try {
        const response = await fetch('http://localhost:53245/api/arbol/insertar', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(parseInt(nodoValor))
        });

        if (response.ok) {
            alert('Nodo insertado y balanceado.');
            inicializar('preorden'); // Volver a cargar el árbol para reflejar los cambios
        } else {
            alert('Error al insertar el nodo.');
        }
    } catch (error) {
        console.error('Error:', error);
        alert('Error al insertar el nodo.');
    }
}


// Función para buscar un nodo en el árbol
async function buscarNodo() {
    const nodoValor = document.getElementById('nodoBuscar').value;
    if (!nodoValor) {
        alert('Por favor ingrese un valor para buscar.');
        return;
    }

    try {
        const response = await fetch(`http://localhost:53245/api/arbol/buscar/${nodoValor}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        });

        if (response.ok) {
            const nodo = await response.json();
            if (nodo) {
                alert(`Nodo encontrado: ${JSON.stringify(nodo)}`);
            } else {
                alert('Nodo no encontrado.');
            }
        } else {
            alert('Error al buscar el nodo.');
        }
    } catch (error) {
        console.error('Error:', error);
        alert('Error al buscar el nodo.');
    }
}

// Función para eliminar un nodo del árbol
async function eliminarNodo() {
    const nodoValor = document.getElementById('nodoEliminar').value;
    if (!nodoValor) {
        alert('Por favor ingrese un valor para eliminar.');
        return;
    }

    try {
        const response = await fetch(`http://localhost:53245/api/arbol/eliminar/${nodoValor}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json'
            }
        });

        if (response.ok) {
            alert('Nodo eliminado.');
            inicializar('preorden'); // Volver a cargar el árbol para reflejar los cambios
        } else {
            alert('Error al eliminar el nodo.');
        }
    } catch (error) {
        console.error('Error:', error);
        alert('Error al eliminar el nodo.');
    }
}

//Función para mostrar Minimo
async function mostrarMinimo() {
    const response = await fetch('http://localhost:53245/api/arbol/busmini');
    if (response.ok) {
        const data = await response.json();
        alert(data);
    } else {
        alert('Error al mostrar el valor Minimo');
    }
}

// Funciones para mostrar el árbol en diferentes órdenes
async function mostrarPreOrden() {
    await inicializar('preorden');
}

async function mostrarOrden() {
    await inicializar('orden');
}

async function mostrarPosorden() {
    await inicializar('posorden');
}

async function mostrarPorNiveles() {
    await inicializar('porniveles');
}

// Función para dibujar el árbol
function dibujarArbol(nodo, x, y, nivel) {
    if (!nodo) return;

    ctx.beginPath();
    ctx.arc(x, y, radius, 0, 2 * Math.PI);
    ctx.fillStyle = 'white';
    ctx.fill();
    ctx.stroke();
    ctx.fillStyle = 'black';
    ctx.fillText(nodo.valor, x - 5, y + 5);

    if (nodo.izquierda) {
        ctx.beginPath();
        ctx.moveTo(x, y);
        ctx.lineTo(x - (horizontalGap * (1 / (nivel + 1))), y + verticalGap);
        ctx.stroke();
        dibujarArbol(nodo.izquierda, x - (horizontalGap * (1 / (nivel + 1))), y + verticalGap, nivel + 1);
    }

    if (nodo.derecha) {
        ctx.beginPath();
        ctx.moveTo(x, y);
        ctx.lineTo(x + (horizontalGap * (1 / (nivel + 1))), y + verticalGap);
        ctx.stroke();
        dibujarArbol(nodo.derecha, x + (horizontalGap * (1 / (nivel + 1))), y + verticalGap, nivel + 1);
    }
}

// Función para inicializar el canvas y dibujar el árbol
async function inicializar(tipo) {
    const arbol = await obtenerArbol(tipo);
    ctx.clearRect(0, 0, canvas.width, canvas.height);
    dibujarArbol(arbol, canvas.width / 2, 50, 0);
    console.log("simp"); 
}

// Inicializar la visualización del árbol
window.onload = () => inicializar('preorden');
