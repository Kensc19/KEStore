// import React from "react"
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import { ProductItem } from './layout';
import { totalPriceNoTax, totalPriceTax,getCartShopStorage,setCartShopStorage } from './page'; //precios totales - manejor LocalStorage


//Creamos la interfaz que deben seguir los props (o parametros) para el componente Modal
interface ModalCartProps {
    show: boolean;
    handleClose: () => void;
    allProduct: ProductItem[];
    //recibir todos los productos actuales del carrito
    setAllProduct: React.Dispatch<React.SetStateAction<ProductItem[]>>;         
    totalWithTax:number;
    setTotalWithTax: React.Dispatch<React.SetStateAction<number>>;
    totalWithNoTax: number;
    setTotalWithNoTax: React.Dispatch<React.SetStateAction<number>>;
  }
  
export const ModalCart: React.FC<ModalCartProps> = ({ show, handleClose,allProduct,setAllProduct,totalWithTax,setTotalWithTax,totalWithNoTax,setTotalWithNoTax }) => {


    const starBuying = () =>{

    }
        

    return (
        <>     
            <Modal show={show} onHide={handleClose} animation={false}>
                <Modal.Header closeButton>
                    <Modal.Title>
                        <div className="cart_title_btn">
                            <h4><i className="fas fa-shopping-cart"></i>Tu Carrito:</h4>                    
                        </div>
                    </Modal.Title>
                </Modal.Header>
                <Modal.Body>

                    <div className="product-menu-cart">

                        {allProduct.map((productItem, index) => (
                            //Tecnica rapida para evitar colocar otro div
                            <>                    
                                <div key={productItem.id}>
                                    <img src={productItem.imageUrl} alt="" />
                                    <p>{productItem.name}</p>
                                    <p><span>Cantidad:</span> {productItem.quantity}</p>
                                    <p><span>Precio:</span> ₡{productItem.price}</p>
                                    <button>Eliminar</button>
                                </div>
                                <hr></hr>
                            </>
                        ))}                
                    </div>                    
                

                </Modal.Body>
                
                <div className="total-price-container">
                    <div className="tax-price-cart total-price-cart">Total: <span>₡{totalWithTax}</span></div>    
                    <hr></hr>
                    <div className="notax-price-cart total-price-cart">Total sin impuestos: <span>₡{totalWithNoTax}</span></div>    
                </div>
                <Modal.Footer>
                    {
                        allProduct.length ? (
                            <>
                                <Button variant="secondary">
                                    Iniciar compra
                                </Button>                              
                            </>
                        ) : (
                            <></>
                        )
                    }
                    <Button variant="secondary">
                        Vaciar Carrito
                    </Button>          
                    <Button variant="secondary" onClick={handleClose}>
                        Cerrar
                    </Button>                    
                    
                </Modal.Footer>
            </Modal>
        </>
    );
}