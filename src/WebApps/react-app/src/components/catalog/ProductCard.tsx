import { makeStyles, createStyles } from '@material-ui/core/styles';
import Card from '@material-ui/core/Card';
import CardMedia from '@material-ui/core/CardMedia';
import CardContent from '@material-ui/core/CardContent';
import Typography from '@material-ui/core/Typography';
import Rating from '@material-ui/lab/Rating';
import { Button, CardActions, CardHeader, IconButton } from '@material-ui/core';
import { IProduct } from '../../common/models/product';
import { useDispatch } from 'react-redux';
import { addRatingRequest } from './state/actions/actions';

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
        iconButton: {
            marginLeft: 'auto',
        },
        ratingContainer: {
            display: 'flex',
            alignItems: 'center',
        },
        averageRating: {
            marginRight: '5px',
        },
    })
);

interface IProps {
    product: IProduct;
}

const ProductCard = ({ product }: IProps) => {
    const classes = useStyles();
    const dispatch = useDispatch();

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
                <IconButton className={classes.iconButton} aria-label="show more">
                    <div className={classes.ratingContainer}>
                        <div className={classes.averageRating}>{product.averageRating}</div>
                        <Rating
                            name={product.id}
                            value={product.averageRating}
                            onChange={(event, newValue) => {
                                dispatch(addRatingRequest(product.id, newValue));
                            }}
                        />
                    </div>
                </IconButton>
            </CardActions>
        </Card>
    );
};

export default ProductCard;
