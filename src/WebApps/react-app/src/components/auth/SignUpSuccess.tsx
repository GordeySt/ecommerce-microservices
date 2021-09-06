import { Button, createStyles, makeStyles, Typography } from '@material-ui/core';
import { useDispatch } from 'react-redux';
import Loader from '../../common/layout/Loader';
import { getLoadingStatus } from '../../common/state/selectors/loaderSelectors';
import { useQuery, useTypedSelector } from '../../common/utils/hooks';
import { resendEmailVerificationRequest } from './state/actions/actions';

const useStyles = makeStyles(() =>
    createStyles({
        infoBlock: {
            textAlign: 'center',
        },
    })
);

const SignUpSuccess = () => {
    const email = useQuery().get('email') as string;
    const classes = useStyles();

    const dispatch = useDispatch();
    const loading = useTypedSelector(getLoadingStatus);

    if (loading) {
        return <Loader />;
    }

    const handleResendEmailVerification = () => {
        dispatch(resendEmailVerificationRequest(email));
    };

    return (
        <div className={classes.infoBlock}>
            <Typography>Successfully registered!</Typography>
            <Typography>Please check your email for the verification</Typography>
            {email && (
                <div>
                    <p>Click button below to resend verification</p>
                    <Button size="medium" onClick={handleResendEmailVerification} variant="contained" color="primary">
                        Resend
                    </Button>
                </div>
            )}
        </div>
    );
};

export default SignUpSuccess;
