-- Supermarket Retail Management System (SRMS) Database Schema

CREATE TABLE roles (
    role_id INT AUTO_INCREMENT PRIMARY KEY,
    role_name VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE users (
    user_id INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(50) NOT NULL UNIQUE,
    password_hash VARCHAR(255) NOT NULL,
    role_id INT,
    full_name VARCHAR(100),
    status VARCHAR(20) DEFAULT 'Active',
    FOREIGN KEY (role_id) REFERENCES roles(role_id)
);

CREATE TABLE categories (
    category_id INT AUTO_INCREMENT PRIMARY KEY,
    category_name VARCHAR(100) NOT NULL UNIQUE,
    description TEXT
);

CREATE TABLE brands (
    brand_id INT AUTO_INCREMENT PRIMARY KEY,
    brand_name VARCHAR(100) NOT NULL UNIQUE,
    description TEXT
);

CREATE TABLE products (
    product_id INT AUTO_INCREMENT PRIMARY KEY,
    product_name VARCHAR(200) NOT NULL,
    barcode VARCHAR(50) UNIQUE,
    sku VARCHAR(50) UNIQUE,
    category_id INT,
    brand_id INT,
    unit VARCHAR(20),
    cost_price DECIMAL(18, 2),
    selling_price DECIMAL(18, 2),
    tax_rate DECIMAL(5, 2),
    status VARCHAR(20) DEFAULT 'Active',
    image_path VARCHAR(255),
    FOREIGN KEY (category_id) REFERENCES categories(category_id),
    FOREIGN KEY (brand_id) REFERENCES brands(brand_id)
);

CREATE TABLE suppliers (
    supplier_id INT AUTO_INCREMENT PRIMARY KEY,
    supplier_name VARCHAR(200) NOT NULL,
    contact_person VARCHAR(100),
    phone VARCHAR(20),
    email VARCHAR(100),
    address TEXT
);

CREATE TABLE purchase_orders (
    po_id INT AUTO_INCREMENT PRIMARY KEY,
    supplier_id INT,
    order_date DATETIME DEFAULT CURRENT_TIMESTAMP,
    total_amount DECIMAL(18, 2),
    status VARCHAR(20) DEFAULT 'Pending',
    FOREIGN KEY (supplier_id) REFERENCES suppliers(supplier_id)
);

CREATE TABLE purchase_order_items (
    po_item_id INT AUTO_INCREMENT PRIMARY KEY,
    po_id INT,
    product_id INT,
    quantity INT,
    unit_cost DECIMAL(18, 2),
    FOREIGN KEY (po_id) REFERENCES purchase_orders(po_id),
    FOREIGN KEY (product_id) REFERENCES products(product_id)
);

CREATE TABLE inventory_batches (
    batch_id INT AUTO_INCREMENT PRIMARY KEY,
    product_id INT,
    batch_number VARCHAR(50),
    quantity INT,
    cost_price DECIMAL(18, 2),
    received_date DATETIME DEFAULT CURRENT_TIMESTAMP,
    expiry_date DATE,
    FOREIGN KEY (product_id) REFERENCES products(product_id)
);

CREATE TABLE stock_history (
    history_id INT AUTO_INCREMENT PRIMARY KEY,
    product_id INT,
    change_type VARCHAR(50), -- e.g., 'Sale', 'Purchase', 'Adjustment', 'Return'
    quantity_changed INT,
    reference_id INT, -- e.g., sale_id or po_id
    change_date DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (product_id) REFERENCES products(product_id)
);

CREATE TABLE customers (
    customer_id INT AUTO_INCREMENT PRIMARY KEY,
    full_name VARCHAR(100) NOT NULL,
    phone VARCHAR(20) UNIQUE,
    email VARCHAR(100),
    loyalty_points INT DEFAULT 0,
    wallet_balance DECIMAL(18, 2) DEFAULT 0.00
);

CREATE TABLE sales (
    sale_id INT AUTO_INCREMENT PRIMARY KEY,
    customer_id INT,
    user_id INT, -- Cashier
    sale_date DATETIME DEFAULT CURRENT_TIMESTAMP,
    total_amount DECIMAL(18, 2),
    discount_amount DECIMAL(18, 2),
    tax_amount DECIMAL(18, 2),
    net_amount DECIMAL(18, 2),
    payment_method VARCHAR(50),
    FOREIGN KEY (customer_id) REFERENCES customers(customer_id),
    FOREIGN KEY (user_id) REFERENCES users(user_id)
);

CREATE TABLE sale_items (
    sale_item_id INT AUTO_INCREMENT PRIMARY KEY,
    sale_id INT,
    product_id INT,
    quantity INT,
    unit_price DECIMAL(18, 2),
    discount DECIMAL(18, 2),
    total_price DECIMAL(18, 2),
    FOREIGN KEY (sale_id) REFERENCES sales(sale_id),
    FOREIGN KEY (product_id) REFERENCES products(product_id)
);

CREATE TABLE payments (
    payment_id INT AUTO_INCREMENT PRIMARY KEY,
    sale_id INT,
    payment_date DATETIME DEFAULT CURRENT_TIMESTAMP,
    amount DECIMAL(18, 2),
    payment_mode VARCHAR(50), -- 'Cash', 'Card', 'QR', 'Split'
    transaction_ref VARCHAR(100),
    FOREIGN KEY (sale_id) REFERENCES sales(sale_id)
);

CREATE TABLE offers (
    offer_id INT AUTO_INCREMENT PRIMARY KEY,
    offer_name VARCHAR(100),
    offer_type VARCHAR(50), -- 'Percentage', 'Fixed', 'BOGO', 'Combo'
    discount_value DECIMAL(18, 2),
    start_date DATE,
    end_date DATE,
    status VARCHAR(20) DEFAULT 'Active'
);

CREATE TABLE audit_logs (
    log_id INT AUTO_INCREMENT PRIMARY KEY,
    user_id INT,
    action VARCHAR(255),
    table_name VARCHAR(50),
    record_id INT,
    action_timestamp DATETIME DEFAULT CURRENT_TIMESTAMP,
    details TEXT,
    FOREIGN KEY (user_id) REFERENCES users(user_id)
);

-- Seed Initial Roles
INSERT INTO roles (role_name) VALUES ('Admin'), ('Manager'), ('Cashier'), ('Inventory Staff');
