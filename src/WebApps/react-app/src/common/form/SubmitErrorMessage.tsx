import { createStyles, makeStyles } from '@material-ui/core'

interface IProps {
    errors: any
}

const useStyles = makeStyles(() =>
    createStyles({
        errorsList: {
            color: 'red',
        },
    })
)

export const SubmitErrorMessage: React.FC<IProps> = ({ errors }) => {
    const classes = useStyles()
    return (
        <ul className={classes.errorsList}>
            {errors.data && Object.keys(errors.data.errors).length > 0 && (
                <>
                    {Object.values(errors.data.errors)
                        .flat()
                        .map((err: any, i) => (
                            <li key={i}>{err}</li>
                        ))}
                </>
            )}
        </ul>
    )
}
