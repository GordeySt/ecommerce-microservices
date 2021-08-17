﻿import { Button, Typography } from '@material-ui/core'
import React from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { Loader } from '../common/layout/Loader'
import { RootState } from '../common/state/store/commonStore'
import { useQuery } from '../common/utils/hooks'
import { resendEmailVerificationRequest } from './state/actions/actions'

export const SignUpSuccess: React.FC = () => {
    const email = useQuery().get('email') as string

    const dispatch = useDispatch()
    const loading = useSelector((state: RootState) => state.loader.loading)

    if (loading) return <Loader />

    const handleResendEmailVerification = () => {
        dispatch(resendEmailVerificationRequest(email))
    }

    return (
        <div style={{ textAlign: 'center' }}>
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
    )
}
