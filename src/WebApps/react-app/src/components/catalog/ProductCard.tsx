import { makeStyles, createStyles } from '@material-ui/core/styles';
import Card from '@material-ui/core/Card';
import CardMedia from '@material-ui/core/CardMedia';
import CardContent from '@material-ui/core/CardContent';
import Typography from '@material-ui/core/Typography';
import Rating from '@material-ui/lab/Rating';
import { Button, CardActions, CardHeader, IconButton } from '@material-ui/core';
import { IProduct } from '../../common/models/product';
import { useDispatch } from 'react-redux';
import { addRatingRequest, changeRatingRequest } from './state/actions/actions';
import { IRatingUser } from '../../common/models/user';
import { useCallback, useEffect, useState } from 'react';
import { IProductRating } from '../../common/models/rating';

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
        allRatingsContainer: {
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',
        },
        ratingBlock: {
            fontSize: '17px',
            display: 'inherit',
        },
        averageRating: {
            marginRight: '5px',
        },
    })
);

interface IProps {
    product: IProduct;
    user: IRatingUser;
}

const ProductCard = ({ product, user }: IProps) => {
    const classes = useStyles();
    const dispatch = useDispatch();
    const [userRating, setUserRating] = useState<IProductRating | null>(null);

    const FindUserRating = useCallback(() => {
        user.ratings?.map((rating) => {
            if (rating.product?.id === product.id) {
                setUserRating(rating);
            }
        });
    }, [product.id, user.ratings]);

    useEffect(() => {
        FindUserRating();
    }, [FindUserRating]);

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
                    <div className={classes.allRatingsContainer}>
                        <div className={classes.ratingContainer}>
                            <div className={classes.ratingBlock}>
                                <span className={classes.averageRating}>Your rating: </span>
                                <div className={classes.averageRating}>{userRating?.rating || 0}</div>
                            </div>
                            <Rating
                                name={product.id}
                                onChange={(event, newValue) => {
                                    userRating
                                        ? dispatch(changeRatingRequest(product.id, newValue))
                                        : dispatch(addRatingRequest(product.id, newValue));

                                    const newUserRating: IProductRating = {
                                        ...userRating,
                                        rating: newValue,
                                    };

                                    setUserRating(newUserRating);
                                }}
                                value={userRating?.rating || 0}
                            />
                        </div>
                        <div className={classes.ratingContainer}>
                            <div className={classes.ratingBlock}>
                                <span className={classes.averageRating}>Avg rating: </span>
                                <div className={classes.averageRating}>{+product.averageRating.toFixed(1)}</div>
                            </div>
                            <Rating name="read-only" value={product.averageRating} precision={0.5} readOnly />
                        </div>
                    </div>
                </IconButton>
            </CardActions>
        </Card>
    );
};

export default ProductCard;
