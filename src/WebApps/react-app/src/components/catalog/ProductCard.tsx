import { makeStyles, createStyles } from '@material-ui/core/styles';
import Card from '@material-ui/core/Card';
import CardMedia from '@material-ui/core/CardMedia';
import CardContent from '@material-ui/core/CardContent';
import Typography from '@material-ui/core/Typography';
import Rating from '@material-ui/lab/Rating';
import { Button, CardActions, CardHeader, IconButton } from '@material-ui/core';
import { IProduct } from '../../common/models/product';

const useStyles = makeStyles(() =>
    createStyles({
        root: {
            maxWidth: 500,
            marginTop: '15px',
        },
        media: {
            width: '500px',
            height: '260px',
            objectFit: 'contain',
        },
    })
);

interface IProps {
    product: IProduct;
}

const ProductCard = ({ product }: IProps) => {
    const classes = useStyles();
    return (
        <Card className={classes.root}>
            <CardHeader action={<div>{product.ageRating}+</div>} title={product.name} subheader={product.price + '$'} />
            <CardMedia
                className={classes.media}
                image={product.mainImageUrl || 'assets/images/no-image.jpg'}
                title="Paella dish"
            />
            <CardContent>
                <Typography variant="body2" color="textSecondary" component="p">
                    {product.summary}
                </Typography>
            </CardContent>
            <CardActions disableSpacing>
                <Button size="medium">Learn More</Button>
                <IconButton style={{ marginLeft: 'auto' }} aria-label="show more">
                    <div style={{ display: 'flex', alignItems: 'center' }}>
                        <div style={{ marginRight: '5px' }}>{product.averageRating}</div>
                        <Rating name="simple-controlled" value={product.averageRating} precision={0.1} />
                    </div>
                </IconButton>
            </CardActions>
        </Card>
    );
};

export default ProductCard;
