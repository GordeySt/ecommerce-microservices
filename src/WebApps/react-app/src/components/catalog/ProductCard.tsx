import { makeStyles, createStyles } from '@material-ui/core/styles';
import Card from '@material-ui/core/Card';
import CardMedia from '@material-ui/core/CardMedia';
import CardContent from '@material-ui/core/CardContent';
import Typography from '@material-ui/core/Typography';
import Rating from '@material-ui/lab/Rating';
import { Button, CardActions, CardHeader, IconButton } from '@material-ui/core';
import { IProduct } from '../../common/models/product';
import { IRatingUser } from '../../common/models/user';
import { useEffect } from 'react';
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

export interface IProductCardProps {
    product: IProduct;
    user: IRatingUser;
    userRating: IProductRating | null;
    findUserRating: () => void;
    // eslint-disable-next-line no-unused-vars
    onRatingChange: (newValue: number | null) => void;
}

const ProductCard = (props: IProductCardProps) => {
    const classes = useStyles();
    const {
        product: { ageRating, averageRating, name, price, summary, mainImageUrl, id },
        userRating,
        findUserRating,
        onRatingChange,
    } = props;

    useEffect(() => {
        findUserRating();
    }, [findUserRating]);

    return (
        <Card className={classes.root}>
            <CardHeader action={<div>{ageRating}+</div>} title={name} subheader={price + '$'} />
            <CardMedia
                className={classes.media}
                image={mainImageUrl || 'assets/images/no-image.jpg'}
                title="Paella dish"
            />
            <CardContent>
                <Typography variant="body2" color="textSecondary" component="p">
                    {summary}
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
                                name={id}
                                onChange={(event, newValue) => {
                                    onRatingChange(newValue);
                                }}
                                value={userRating?.rating || 0}
                            />
                        </div>
                        <div className={classes.ratingContainer}>
                            <div className={classes.ratingBlock}>
                                <span className={classes.averageRating}>Avg rating: </span>
                                <div className={classes.averageRating}>{+averageRating.toFixed(1)}</div>
                            </div>
                            <Rating name="read-only" value={averageRating} precision={0.5} readOnly />
                        </div>
                    </div>
                </IconButton>
            </CardActions>
        </Card>
    );
};

export default ProductCard;
