import { useEffect } from 'react'
import { useHistory } from 'react-router'
import { useQuery, useTypedSelector } from '../../common/utils/hooks'
import { resendEmailVerificationRequest, verifyEmailRequest } from './state/actions/actions'
import { Button, createStyles, makeStyles } from '@material-ui/core'
import { useDispatch } from 'react-redux'
import { CommonRoutes } from '../../common/constants/routeConstants'
import Loader from '../../common/layout/Loader'
import { getLoadingStatus } from '../../common/state/selectors/loaderSelectors'

const useStyles = makeStyles(() =>
    createStyles({
        infoBlock: {
            textAlign: 'center',
        },
        buttons: {
            display: 'flex',
            justifyContent: 'center',
            marginTop: 10,
        },
        resendButton: {
            marginRight: 10,
        },
    })
)

const VerifyEmail = () => {
    const dispatch = useDispatch()
    const history = useHistory()
    const classes = useStyles()
    const token = useQuery().get('token') as string
    const email = useQuery().get('email') as string
    const loading = useTypedSelector(getLoadingStatus)

    useEffect(() => {
        dispatch(verifyEmailRequest(email, token))
    }, [dispatch, email, token])

    const handleResendEmailVerification = () => {
        dispatch(resendEmailVerificationRequest(email))
    }

    if (loading) {
        return <Loader />
    }

    return (
        <div className={classes.infoBlock}>
            <div>Email Verification</div>
            <div className={classes.buttons}>
                <div className={classes.resendButton}>
                    <Button size="medium" onClick={handleResendEmailVerification} variant="contained" color="primary">
                        Resend
                    </Button>
                </div>
                <div>
                    <Button
                        size="medium"
                        onClick={() => history.push(CommonRoutes.defaultRoute)}
                        variant="contained"
                        color="primary"
                    >
                        Go back
                    </Button>
                </div>
            </div>
        </div>
    )
}

export default VerifyEmail
