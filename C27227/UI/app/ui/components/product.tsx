import React from 'react';
import '../Styles/product.css';

const Product = ({ product, handleClick }) => {
  if (product === undefined || handleClick === undefined) {
    throw new Error("Product or handleClick is undefined");
  }
  const { name, imageUrl, price, description } = product;
  if (!name || !imageUrl || price === undefined || !description) {
    throw new Error("Required product attributes are undefined");
  }

  return (
    <div className="col-md-3 mb-4">
      <div className="border p-4 product-container">
        <h3>{name}</h3>
        <p>$ {price}</p>
        <img src={imageUrl} alt={name} />
        <p className="product-description" dangerouslySetInnerHTML={{ __html: description }}></p>
        <button className="add-to-cart-btn" onClick={() => handleClick(product)}>Add to Cart</button>
      </div>
    </div>
  );
};

export default Product;
