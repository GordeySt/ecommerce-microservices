import { ProductsList } from '../../components/catalog/ProductsList';

const CatalogPage = () => {
    return (
        <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', flexDirection: 'column' }}>
            <ProductsList />
        </div>
    );
};

export default CatalogPage;
